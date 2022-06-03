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
    public async Task<string> MintNft(EthereumNetwork chain,
        string destinationAddress,
        string tokenUri,
        string name,
        string symbol,
        double amount)
    {
        var cluster = chain switch
        {
            EthereumNetwork.SolanaMainnet => Cluster.MainNet,
            EthereumNetwork.SolanaDevnet => Cluster.DevNet,
            EthereumNetwork.SolanaTestnet => Cluster.TestNet,
            _ => throw new ArgumentOutOfRangeException(nameof(chain), chain, null)
        };

        var client = ClientFactory.GetClient(cluster);

        var wallet = CreateNewWallet();
        ulong tokenPrice = 20000000; // 1 SOL

        var airdropSig = await client.RequestAirdropAsync(wallet.Account.PublicKey, 50000000);

        var airDropCompleted = false;
        do
        {
            var transaction = await client.GetTransactionAsync(airdropSig.Result);

            airDropCompleted = transaction.WasRequestSuccessfullyHandled && transaction.ErrorData == null;
        } while (!airDropCompleted);

        var walletAddress = wallet.Account.PublicKey;
        var balanceRes = await client.GetBalanceAsync(walletAddress);

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
            symbol = "DNFT",
            uri = tokenUri,
            creators = new List<Creator> { new Creator(walletAddress, 100, true) },
            sellerFeeBasisPoints = 88,
        };

        var destinationPublicKey = new PublicKey(destinationAddress);

        var pipeline = new SolanaTransactionInstructionsPipeline();

        pipeline.InitializeForMint(walletAddress, destinationPublicKey, mintAddress, rentExemption.Result, (ulong)amount, tokenPrice);
        pipeline.AddMetadata(walletAddress, mintAddress, metadataAddress, data);
        pipeline.AddMasterEdition(walletAddress, mintAddress, masterEditionAddress, metadataAddress, data);

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
            return string.Empty;
        }

        var txResult = await client.SendTransactionAsync(tx);

        return txResult.Result;
    }

    private Wallet CreateNewWallet()
    {
        var newMnemonic = new Mnemonic(WordList.English, WordCount.Twelve);
        return new Wallet(newMnemonic);
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
