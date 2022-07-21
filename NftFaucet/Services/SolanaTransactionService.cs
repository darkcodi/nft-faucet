using System.Text;
using NftFaucet.Models.Enums;
using Solana.Metaplex;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Models;
using Solnet.Rpc.Types;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;

namespace NftFaucet.Services;
public class SolanaTransactionService : ISolanaTransactionService
{
    public async Task<string> MintNft(NetworkChain chain,
        string destinationAddress,
        string tokenUri,
        string name,
        string symbol,
        bool isTokenMutable,
        bool includeMasterEdition,
        uint sellerFeeBasisPoints,
        ulong amount)
    {
        var cluster = chain switch
        {
            NetworkChain.SolanaMainnet => Cluster.MainNet,
            NetworkChain.SolanaDevnet => Cluster.DevNet,
            NetworkChain.SolanaTestnet => Cluster.TestNet,
            _ => throw new ArgumentOutOfRangeException(nameof(chain), chain, null)
        };

        var client = ClientFactory.GetClient(cluster);

        var wallet = CreateNewWallet();
        ulong tokenPrice = 20000000; // 0.02 SOL

        var airdropSig = await client.RequestAirdropAsync(wallet.Account.PublicKey, 50000000);
        Console.WriteLine(airdropSig.Result);

        await RepeatAsync(async () =>
        {
            var transaction = await client.GetTransactionAsync(airdropSig.Result);

            return transaction.WasRequestSuccessfullyHandled && transaction.ErrorData == null;
        }, TimeSpan.FromSeconds(1), 100);

        var walletAddress = wallet.Account.PublicKey;

        await RepeatAsync(async () =>
        {
            var balanceRes = await client.GetBalanceAsync(walletAddress);

            return balanceRes.Result.Value > 0;
        }, TimeSpan.FromSeconds(1), 5);


        var rentExemption = await client.GetMinimumBalanceForRentExemptionAsync(
            TokenProgram.MintAccountDataSize,
            Solnet.Rpc.Types.Commitment.Confirmed
        );

        var mint = wallet.GetAccount(1);
        var mintAddress = mint.PublicKey;
        var metadataAddress = GetMetadataAddress(mint.PublicKey);
        var masterEditionAddress = GetMasterEditionAddress(mint.PublicKey);

        // TOKEN METADATA
        var data = new MetadataParameters()
        {
            name = name,
            symbol = symbol,
            uri = tokenUri,
            creators = new List<Creator> { new Creator(walletAddress, 100, true) },
            sellerFeeBasisPoints = sellerFeeBasisPoints,
        };

        var destinationPublicKey = new PublicKey(destinationAddress);

        var pipeline = new SolanaTransactionInstructionsPipeline();

        pipeline.InitializeForMint(walletAddress, destinationPublicKey, mintAddress, rentExemption.Result, (ulong)amount, tokenPrice);
        pipeline.AddMetadata(walletAddress, mintAddress, metadataAddress, data, isTokenMutable);

        if (includeMasterEdition)
        {
            pipeline.AddMasterEdition(walletAddress, mintAddress, masterEditionAddress, metadataAddress, data);
        }

        var blockHash = (await client.GetRecentBlockHashAsync()).Result.Value.Blockhash;

        var txBuilder = new TransactionBuilder()
            .SetFeePayer(wallet.Account.PublicKey)
            .SetRecentBlockHash(blockHash);

        pipeline.Build(txBuilder);

        var tx = txBuilder.Build(new List<Account> { wallet.Account, mint });
        var simulationResult = await client.SimulateTransactionAsync(tx);
        var isSimulationSuccessful = simulationResult.ErrorData == null;

        if (!isSimulationSuccessful)
        {
            throw new Exception("Transaction simulation failed. Try again.");
        }

        var txResult = await client.SendTransactionAsync(tx);

        return txResult.Result;
    }

    private Wallet CreateNewWallet()
    {
        var newMnemonic = new Mnemonic(WordList.English, WordCount.Twelve);
        return new Wallet(newMnemonic);
    }

    public async Task RepeatAsync(Func<Task<bool>> work, TimeSpan retryInterval, int maxExecutionCount = 3)
    {
        for (var i = 0; i < maxExecutionCount; ++i)
        {
            try
            {
                var result = await work();
                if (result)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
            }

            await Task.Delay(retryInterval);
        }
    }

    private PublicKey GetMetadataAddress(PublicKey mintAddress)
    {
        // PDA METADATA
        AddressExtensions.TryFindProgramAddress(
            new List<byte[]>() {
                Encoding.UTF8.GetBytes("metadata"),
                MetadataProgram.ProgramIdKey,
                mintAddress
            },
            MetadataProgram.ProgramIdKey,
            out var metadataAddressBytes,
            out _
        );
        var metadataAddress = new PublicKey(metadataAddressBytes);

        return metadataAddress;
    }

    private PublicKey GetMasterEditionAddress(PublicKey mintAddress)
    {
        // PDA MASTER EDITION
        AddressExtensions.TryFindProgramAddress(
            new List<byte[]>() {
                Encoding.UTF8.GetBytes("metadata"),
                MetadataProgram.ProgramIdKey,
                mintAddress,
                Encoding.UTF8.GetBytes("edition")
            },
            MetadataProgram.ProgramIdKey,
            out var masterEditionAddressBytes,
            out _
        );
        var masterEditionAddress = new PublicKey(masterEditionAddressBytes);

        return masterEditionAddress;
    }
}
