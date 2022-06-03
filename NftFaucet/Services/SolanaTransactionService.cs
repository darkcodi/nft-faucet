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

        // Generate a new mnemonic
        var newMnemonic = new Mnemonic(WordList.English, WordCount.Twelve);

        var wallet = new Wallet(newMnemonic);
        var derIndex = 1;
        ulong tokenPrice = 20000000; // 1 SOL

        var sig = await client.RequestAirdropAsync(wallet.Account.PublicKey, 50000000);

        await client.GetConfirmedTransactionAsync(sig.Result);

        var walletAddress = wallet.Account.PublicKey;
        Console.WriteLine($"Using account public key : {walletAddress}");

        var balanceRes = await client.GetBalanceAsync(walletAddress);
        if (balanceRes.WasSuccessful)
            Console.WriteLine($"Account balance: {balanceRes.Result.Value}");

        var rentExemption = await client.GetMinimumBalanceForRentExemptionAsync(
            TokenProgram.MintAccountDataSize,
            Solnet.Rpc.Types.Commitment.Confirmed
        );
        if (rentExemption.WasSuccessful)
            Console.WriteLine($"Rent exemption: {rentExemption.Result}");

        var mint = wallet.GetAccount(derIndex);
        var mintAddress = mint.PublicKey;
        Console.WriteLine($"Mint: {mintAddress}");

        // PDA METADATA
        AddressExtensions.TryFindProgramAddress(
            new List<byte[]>() {
        Encoding.UTF8.GetBytes("metadata"),
        MetadataProgram.ProgramIdKey,
        mint.PublicKey
            },
            MetadataProgram.ProgramIdKey,
            out var metadataAddressBytes,
            out _
        );
        var metadataAddress = new PublicKey(metadataAddressBytes);
        Console.WriteLine($"PDA METADATA: {metadataAddress}");

        // PDA MASTER EDITION
        AddressExtensions.TryFindProgramAddress(
            new List<byte[]>() {
        Encoding.UTF8.GetBytes("metadata"),
        MetadataProgram.ProgramIdKey,
        mint.PublicKey,
        Encoding.UTF8.GetBytes("edition")
            },
            MetadataProgram.ProgramIdKey,
            out var masterEditionAddressBytes,
            out _
        );
        var masterEditionAddress = new PublicKey(masterEditionAddressBytes);
        Console.WriteLine($"PDA MASTER EDITION: {masterEditionAddress}");

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

        // Step #3 - Associated Token Program: Create
        instructions.Add(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
            walletAddress,
            walletAddress,
            mintAddress
        ));

        // Step #4 - Token Program: Mint To
        // Wallet address = dest
        var destinationPublicKey = new PublicKey(destinationAddress);
        var tokenBalanceAddress = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(walletAddress, mintAddress);
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
        var isSimulationSuccessful = simulationResult.ErrorData == null;
        if (!isSimulationSuccessful)
        {
            Console.WriteLine("========== ERROR ==========");
            Console.WriteLine(simulationResult.RawRpcResponse);
            Console.WriteLine("========== ERROR ==========");
            return string.Empty;
        }

        var txResult = await client.SendTransactionAsync(tx);
        Console.WriteLine("========== SUCCESS ==========");
        Console.WriteLine(txResult.RawRpcResponse);
        Console.WriteLine("========== SUCCESS ==========");

        return txResult.Result;
    }
}
