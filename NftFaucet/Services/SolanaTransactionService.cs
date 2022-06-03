using System.Text;
using NftFaucet.Models.Enums;
using Solana.Metaplex;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Models;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;

namespace NftFaucet.Services;
public class SolanaTransactionService : ISolanaTransactionService
{
    public async Task<string> MintNft(EthereumNetwork chain, string destinationAddress, string tokenUri, string name, double amount)
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
        var derIndex = 1;
        ulong tokenPrice = 20000000; // 1 SOL

        var airdropSig = await client.RequestAirdropAsync(wallet.Account.PublicKey, 50000000);
        await client.GetTransactionAsync(airdropSig.Result);

        var walletAddress = wallet.Account.PublicKey;
        var balanceRes = await client.GetBalanceAsync(walletAddress);

        var rentExemption = await client.GetMinimumBalanceForRentExemptionAsync(
            TokenProgram.MintAccountDataSize,
            Solnet.Rpc.Types.Commitment.Confirmed
        );

        var mint = wallet.GetAccount(derIndex);
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

        var instructions = new List<TransactionInstruction>();

        // Step #1 - System Program: Create Account
        instructions.Add(SystemProgram.CreateAccount(
            walletAddress,
            mintAddress,
            rentExemption.Result,
            TokenProgram.MintAccountDataSize,
            TokenProgram.ProgramIdKey
        ));

        // Step #2 - Token Program: Initialize Mint
        instructions.Add(TokenProgram.InitializeMint(
            mintAddress,
            0,
            walletAddress,
            walletAddress
        ));
        var destinationPublicKey = new PublicKey(destinationAddress);
        // Step #3 - Associated Token Program: Create
        instructions.Add(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
            walletAddress,
            destinationPublicKey,
            mintAddress
        ));

        // Step #4 - Token Program: Mint To
        // Wallet address = dest
        var tokenBalanceAddress = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(destinationPublicKey, mintAddress);
        instructions.Add(TokenProgram.MintTo(
            mintAddress,
            tokenBalanceAddress,
            (ulong)amount,
            walletAddress
        ));

        // Step #5.1 - System Program: Transfer
        instructions.Add(SystemProgram.Transfer(
            walletAddress,
            walletAddress,
            tokenPrice
        ));

        // Step #5.2 (also 5.3, 5.4, 5.5) - Token Metadata Program: Create Metadata Accounts
        instructions.Add(MetadataProgram.CreateMetadataAccount(
            metadataAddress,
            mintAddress,
            walletAddress,
            walletAddress,
            walletAddress,
            data,
            true,
            true
        ));

        // Step #5.6 (also 5.7, 5.8, 5.9, 5.10, 5.11) - Token Metadata Program: Create Master Edition
        instructions.Add(MetadataProgram.CreateMasterEdition(
            1,
            masterEditionAddress,
            mintAddress,
            walletAddress,
            walletAddress,
            walletAddress,
            metadataAddress
        ));

        var blockHash = (await client.GetRecentBlockHashAsync()).Result.Value.Blockhash;

        var txBuilder = new TransactionBuilder()
            .SetFeePayer(wallet.Account.PublicKey)
            .SetRecentBlockHash(blockHash);

        foreach (var instruction in instructions)
        {
            txBuilder.AddInstruction(instruction);
        }

        var tx = txBuilder.Build(new List<Account> { wallet.Account, mint });
        var simulationResult = await client.SimulateTransactionAsync(tx);
        var isSimulationSuccessful = simulationResult.WasSuccessful;

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
