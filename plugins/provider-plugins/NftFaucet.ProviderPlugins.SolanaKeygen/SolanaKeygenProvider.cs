using System.Numerics;
using System.Text;
using CSharpFunctionalExtensions;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Domain.Utils;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.Plugins.Models.Enums;
using Solana.Metaplex;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Models;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;

namespace NftFaucet.ProviderPlugins.SolanaKeygen;

public class SolanaKeygenProvider : Provider
{
    public override Guid Id { get; } = Guid.Parse("4c1a8ac5-60ca-4024-aae6-3c9852a6535c");
    public override string Name { get; } = "Solana keygen";
    public override string ShortName { get; } = "SolKeygen";
    public override string ImageName { get; } = "ecdsa.svg";
    public override bool IsConfigured { get; protected set; }
    public SolanaKey Key { get; private set; }

    public override Property[] GetProperties()
        => new[]
        {
            new Property{ Name = "Address", Value = Key?.Address ?? "<null>" },
        };

    public override ConfigurationItem[] GetConfigurationItems()
    {
        var mnemonicInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Mnemonic phrase",
            Placeholder = "<null>",
            Value = Key?.MnemonicPhrase ?? string.Empty,
            IsDisabled = true,
        };
        var privateKeyInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Private key",
            Placeholder = "<null>",
            Value = Key?.PrivateKey ?? string.Empty,
            IsDisabled = true,
        };
        var addressInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Address",
            Placeholder = "<null>",
            Value = Key?.Address ?? string.Empty,
            IsDisabled = true,
        };
        var button = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Button,
            Name = "Generate new keys",
            Placeholder = "<null>",
            ClickAction = () =>
            {
                var generatedKey = SolanaKey.GenerateNew();
                mnemonicInput.Value = generatedKey.MnemonicPhrase;
                privateKeyInput.Value = generatedKey.PrivateKey;
                addressInput.Value = generatedKey.Address;
            },
        };
        return new[] { mnemonicInput, privateKeyInput, addressInput, button };
    }

    public override Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        var keyResult = ResultWrapper.Wrap(() => new SolanaKey(configurationItems[0].Value));
        if (keyResult.IsFailure)
            return Task.FromResult(Result.Failure(keyResult.Error));

        Key = keyResult.Value;
        IsConfigured = true;
        return Task.FromResult(Result.Success());
    }

    public override bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;

    public override Task<string> GetAddress()
        => Task.FromResult(Key?.Address);

    public override async Task<Balance> GetBalance(INetwork network)
    {
        if (string.IsNullOrEmpty(Key?.Address))
            return null;

        var rpcClient = ClientFactory.GetClient(network.PublicRpcUrl.OriginalString);
        var balanceResult = await rpcClient.GetBalanceAsync(Key.Address);
        if (!balanceResult.WasSuccessful || balanceResult.Result == null)
            return null;

        var balance = new BigInteger(balanceResult.Result.Value);
        return new Balance(balance, "lamport");
    }

    public override Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        if (selectedNetwork != null && selectedNetwork.Type == NetworkType.Solana)
            return Task.FromResult(selectedNetwork);

        var matchingNetwork = allKnownNetworks.FirstOrDefault(x => x.Type == NetworkType.Solana && x.IsSupported) ??
                              allKnownNetworks.FirstOrDefault(x => x.Type == NetworkType.Solana);
        return Task.FromResult(matchingNetwork);
    }

    public override async Task<string> Mint(MintRequest mintRequest)
    {
        var client = ClientFactory.GetClient(mintRequest.Network.PublicRpcUrl.OriginalString);
        var rentExemption = await client.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize);

        var wallet = new Wallet(Key.MnemonicPhrase);
        var walletAddress = wallet.Account.PublicKey;
        var mintWallet = new Wallet(SolanaKey.GenerateNew().MnemonicPhrase);
        var mint = mintWallet.GetAccount(0);
        var metadataAddress = GetMetadataAddress(mint.PublicKey);
        var masterEditionAddress = GetMasterEditionAddress(mint.PublicKey);

        // TOKEN METADATA
        var data = new MetadataParameters()
        {
            name = mintRequest.Token.Name,
            symbol = "SNFT",
            uri = mintRequest.UploadLocation.Location,
            creators = new List<Creator> { new Creator(wallet.Account.PublicKey, 100, true) },
            sellerFeeBasisPoints = 924,
        };

        var destinationPublicKey = new PublicKey(mintRequest.DestinationAddress);

        var instructions = new List<TransactionInstruction>();

        instructions.Add(SystemProgram.CreateAccount(walletAddress, mint, rentExemption.Result, TokenProgram.MintAccountDataSize, TokenProgram.ProgramIdKey));
        instructions.Add(TokenProgram.InitializeMint(mint, 0, walletAddress, walletAddress));
        var tokenBalanceAddress = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(destinationPublicKey, mint);
        var tokenBalanceAddressInfo = await client.GetAccountInfoAsync(tokenBalanceAddress);
        if (!tokenBalanceAddressInfo.WasSuccessful)
        {
            throw new Exception("GetAccountInfo failed: " + (tokenBalanceAddressInfo.Reason ?? "<reason is null>"));
        }

        var tokenBalanceAddressExists = tokenBalanceAddressInfo.Result.Value != null;
        if (!tokenBalanceAddressExists)
        {
            instructions.Add(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(walletAddress, destinationPublicKey, mint));
        }
        instructions.Add(TokenProgram.MintTo(mint, tokenBalanceAddress, (ulong)mintRequest.TokensAmount, walletAddress));

        instructions.Add(MetadataProgram.CreateMetadataAccount(
            metadataAddress,
            mint,
            walletAddress,
            walletAddress,
            walletAddress,
            data,
            true,
            false
        ));
        instructions.Add(MetadataProgram.CreateMasterEdition(
            1,
            masterEditionAddress,
            mint,
            walletAddress,
            walletAddress,
            walletAddress,
            metadataAddress
        ));

        var blockHash = (await client.GetRecentBlockHashAsync()).Result.Value.Blockhash;

        var txBuilder = new TransactionBuilder()
            .SetFeePayer(wallet.Account.PublicKey)
            .SetRecentBlockHash(blockHash);

        instructions.ForEach(x => txBuilder.AddInstruction(x));

        var signers = new List<Account> {wallet.Account, mint};
        var tx = txBuilder.Build(signers);
        var simulationResult = await client.SimulateTransactionAsync(tx);
        if (!simulationResult.WasSuccessful)
        {
            throw new Exception("Simulation failed: " + (simulationResult.Reason ?? "<reason is null>"));
        }
        var isSimulationSuccessful = simulationResult.ErrorData == null;
        if (!isSimulationSuccessful)
        {
            throw new Exception("Transaction simulation failed. Try again.");
        }

        var txResult = await client.SendTransactionAsync(tx);
        if (!txResult.WasSuccessful)
        {
            throw new Exception(txResult.RawRpcResponse);
        }

        return txResult.Result;
    }

    public override Task<string> GetState()
        => Task.FromResult(Key.MnemonicPhrase);

    public override Task SetState(string state)
    {
        if (!string.IsNullOrEmpty(state))
        {
            Key = new SolanaKey(state);
            IsConfigured = true;
        }

        return Task.CompletedTask;
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
