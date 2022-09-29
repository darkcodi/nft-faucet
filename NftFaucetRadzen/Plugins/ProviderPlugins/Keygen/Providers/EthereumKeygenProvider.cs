using System.Numerics;
using CSharpFunctionalExtensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Function;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Utils;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Keygen.Providers;

public class EthereumKeygenProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("ded55b2b-8139-4251-a0fc-ba620f9727c9");
    public string Name { get; } = "Ethereum keygen";
    public string ShortName { get; } = "EthKeygen";
    public string ImageName { get; } = "ecdsa.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured { get; private set; }
    public EthereumKey Key { get; private set; }

    public Task InitializeAsync(IServiceProvider serviceProvider)
        => Task.CompletedTask;

    public CardListItemProperty[] GetProperties()
        => new CardListItemProperty[]
        {
            new CardListItemProperty{ Name = "Address", Value = Key?.Address ?? "<null>" },
        };

    public CardListItemConfiguration GetConfiguration()
    {
        var privateKeyInput = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Private key",
            Placeholder = "<null>",
            Value = Key?.PrivateKey ?? string.Empty,
            IsDisabled = true,
        };
        var addressInput = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Address",
            Placeholder = "<null>",
            Value = Key?.Address ?? string.Empty,
            IsDisabled = true,
        };
        var button = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Button,
            Name = "Generate new keys",
            Placeholder = "<null>",
            ClickAction = () =>
            {
                var generatedKey = EthereumKey.GenerateNew();
                privateKeyInput.Value = generatedKey.PrivateKey;
                addressInput.Value = generatedKey.Address;
            },
        };
        return new CardListItemConfiguration
        {
            Objects = new[] { privateKeyInput, addressInput, button },
            ConfigureAction = objects =>
            {
                var keyResult = ResultWrapper.Wrap(() => new EthereumKey(objects[0].Value));
                if (keyResult.IsFailure)
                    return Task.FromResult(Result.Failure(keyResult.Error));

                Key = keyResult.Value;
                IsConfigured = true;
                return Task.FromResult(Result.Success());
            },
        };
    }

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;

    public Task<string> GetAddress()
        => Task.FromResult(Key?.Address);

    public async Task<long?> GetBalance(INetwork network)
    {
        if (string.IsNullOrEmpty(Key?.Address))
            return null;

        var web3 = new Web3(network.PublicRpcUrl.OriginalString);
        var hexBalance = await web3.Eth.GetBalance.SendRequestAsync(Key.Address);
        var balance = (long) hexBalance.Value;
        return balance;
    }

    public Task<bool> EnsureNetworkMatches(INetwork network)
        => Task.FromResult(network.Type == NetworkType.Ethereum);

    public async Task<Result<string>> Mint(MintRequest mintRequest)
    {
        if (mintRequest.Network.Type != NetworkType.Ethereum)
        {
            throw new InvalidOperationException("Invalid network type for this provider");
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

        return await ResultWrapper.Wrap(async () =>
        {
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
        });
    }

    public Task<string> GetState()
        => Task.FromResult(Key.PrivateKey);

    public Task SetState(string state)
    {
        if (!string.IsNullOrEmpty(state))
        {
            Key = new EthereumKey(state);
            IsConfigured = true;
        }

        return Task.CompletedTask;
    }
}
