using CSharpFunctionalExtensions;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
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
        => Task.FromResult(Key.Address);

    public Task<bool> EnsureNetworkMatches(INetwork network)
        => Task.FromResult(network.Type == NetworkType.Ethereum);

    public Task<Result<string>> Mint(MintRequest mintRequest)
    {
        throw new NotImplementedException();
    }
}
