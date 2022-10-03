using System.Numerics;
using CSharpFunctionalExtensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using NftFaucet.Domain.Function;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Domain.Utils;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.Plugins.Models.Enums;

namespace NftFaucet.WalletPlugins.Keygens;

public sealed class EthereumKeygenWallet : Wallet
{
    public override Guid Id { get; } = Guid.Parse("ded55b2b-8139-4251-a0fc-ba620f9727c9");
    public override string Name { get; } = "Ethereum keygen";
    public override string ShortName { get; } = "EthKeygen";
    public override string ImageName { get; } = "ecdsa.svg";
    public override bool IsConfigured { get; protected set; }
    public EthereumKey Key { get; private set; }

    public override Property[] GetProperties()
        => new[]
        {
            new Property{ Name = "Address", Value = Key?.Address ?? "<null>" },
        };

    public override ConfigurationItem[] GetConfigurationItems()
    {
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
                var generatedKey = EthereumKey.GenerateNew();
                privateKeyInput.Value = generatedKey.PrivateKey;
                addressInput.Value = generatedKey.Address;
            },
        };
        return new[] { privateKeyInput, addressInput, button };
    }

    public override Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        var keyResult = ResultWrapper.Wrap(() => new EthereumKey(configurationItems[0].Value));
        if (keyResult.IsFailure)
            return Task.FromResult(Result.Failure(keyResult.Error));

        Key = keyResult.Value;
        IsConfigured = true;
        return Task.FromResult(Result.Success());
    }

    public override bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;

    public override Task<string> GetAddress()
        => Task.FromResult(Key?.Address);

    public override async Task<Balance> GetBalance(INetwork network)
    {
        if (string.IsNullOrEmpty(Key?.Address))
            return null;

        var web3 = new Web3(network.PublicRpcUrl.OriginalString);
        var hexBalance = await web3.Eth.GetBalance.SendRequestAsync(Key.Address);
        var balance = hexBalance.Value;
        return new Balance(balance, "wei");
    }

    public override Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        if (selectedNetwork != null && selectedNetwork.Type == NetworkType.Ethereum)
            return Task.FromResult(selectedNetwork);

        var matchingNetwork = allKnownNetworks.FirstOrDefault(x => x.Type == NetworkType.Ethereum && x.IsSupported) ??
            allKnownNetworks.FirstOrDefault(x => x.Type == NetworkType.Ethereum);
        return Task.FromResult(matchingNetwork);
    }

    public override async Task<string> Mint(MintRequest mintRequest)
    {
        if (mintRequest.Network.Type != NetworkType.Ethereum)
        {
            throw new InvalidOperationException("Invalid network type for this wallet");
        }

        Function transfer =
            mintRequest.Contract.Type switch
            {
                ContractType.Erc721 => new Erc721MintFunction
                {
                    To = mintRequest.DestinationAddress,
                    Uri = mintRequest.UploadLocation.Location,
                },
                ContractType.Erc1155 => new Erc1155MintFunction
                {
                    To = mintRequest.DestinationAddress,
                    Amount = mintRequest.TokensAmount,
                    Uri = mintRequest.UploadLocation.Location,
                },
                _ => throw new ArgumentOutOfRangeException(),
            };

        var data = transfer.Encode();
        var client = new RpcClient(mintRequest.Network.PublicRpcUrl);
        var account = new Account(Key.PrivateKey);
        var transactionManager = new AccountSignerTransactionManager(client, account);
        var txInput = new TransactionInput
        {
            From = Key.Address,
            To = mintRequest.Contract.Address,
            Data = data,
            ChainId = new HexBigInteger(new BigInteger(mintRequest.Network.ChainId!.Value)),
            Type = new HexBigInteger(TransactionType.EIP1559.AsByte()),
        };
        txInput.Nonce = await transactionManager.GetNonceAsync(txInput);
        var fee1559 = await transactionManager.CalculateFee1559Async();
        txInput.MaxFeePerGas = new HexBigInteger(fee1559.MaxFeePerGas!.Value);
        txInput.MaxPriorityFeePerGas = new HexBigInteger(fee1559.MaxPriorityFeePerGas!.Value);
        txInput.Gas = await transactionManager.EstimateGasAsync(txInput);
        txInput.Gas = new HexBigInteger(new BigInteger((long)txInput.Gas.Value * 1.3));
        var transactionHash = await transactionManager.SendTransactionAsync(txInput);
        if (string.IsNullOrEmpty(transactionHash))
        {
            throw new Exception("Operation was cancelled or RPC node failure");
        }

        return transactionHash;
    }

    public override Task<string> GetState()
        => Task.FromResult(Key.PrivateKey);

    public override Task SetState(string state)
    {
        if (!string.IsNullOrEmpty(state))
        {
            Key = new EthereumKey(state);
            IsConfigured = true;
        }

        return Task.CompletedTask;
    }
}
