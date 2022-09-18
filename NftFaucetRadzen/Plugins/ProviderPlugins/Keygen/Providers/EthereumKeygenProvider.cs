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

    public CardListItemProperty[] GetProperties()
        => new CardListItemProperty[]
        {
            new CardListItemProperty{ Name = "Private key", Value = Key?.PrivateKey ?? "<null>" },
            new CardListItemProperty{ Name = "Address", Value = Key?.Address ?? "<null>" },
        };

    public CardListItemConfiguration GetConfiguration()
    {
        var privateKeyInput = new CardListItemConfigurationObject
        {
            Id = Guid.Parse("5f92930d-7a8f-41e6-aa14-5608185e6f4b"),
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Private key",
            Placeholder = "<null>",
            Value = Key?.PrivateKey ?? string.Empty,
            IsDisabled = true,
        };
        var addressInput = new CardListItemConfigurationObject
        {
            Id = Guid.Parse("be0de328-fc98-46fe-8af5-dfb8414ecc01"),
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Address",
            Placeholder = "<null>",
            Value = Key?.Address ?? string.Empty,
            IsDisabled = true,
        };
        var button = new CardListItemConfigurationObject
        {
            Id = Guid.Parse("cba7789e-188e-405b-80c3-b86da1c17850"),
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
}
