using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Utils;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Keygen.Providers;

public class SolanaKeygenProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("4c1a8ac5-60ca-4024-aae6-3c9852a6535c");
    public string Name { get; } = "Solana keygen";
    public string ShortName { get; } = "SolKeygen";
    public string ImageName { get; } = "ecdsa.svg";
    public bool IsSupported { get; } = true;
    public bool CanBeConfigured { get; } = true;
    public bool IsConfigured { get; private set; }
    public SolanaKey Key { get; private set; }

    public Task Configure(CardListItemConfigurationObject[] items)
    {
        Key = SolanaKey.GenerateNew();
        IsConfigured = true;
        return Task.CompletedTask;
    }

    public CardListItemProperty[] GetProperties()
        => new CardListItemProperty[]
        {
            new CardListItemProperty{ Name = "Private key", Value = Key?.PrivateKey ?? "<null>" },
            new CardListItemProperty{ Name = "Address", Value = Key?.Address ?? "<null>" },
        };

    public CardListItemConfiguration GetConfiguration()
    {
        var input = new CardListItemConfigurationObject
        {
            Id = Guid.Parse("17c198eb-4635-4229-a86f-051dcd7ca440"),
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Private key",
            Value = Key?.PrivateKey ?? string.Empty,
        };
        var button = new CardListItemConfigurationObject
        {
            Id = Guid.Parse("6eeb1400-aae0-46c1-ab94-ae80029ce5cb"),
            Type = CardListItemConfigurationObjectType.Button,
            Name = "Generate new keys",
            ClickAction = () => input.Value = SolanaKey.GenerateNew().PrivateKey,
        };
        return new CardListItemConfiguration
        {
            Objects = new[] { input, button },
            ValidationFunc = objects => Task.FromResult(ResultWrapper.Wrap(() => new SolanaKey(input.Value)).IsSuccess),
            ConfigureAction = objects =>
            {
                Key = new SolanaKey(input.Value);
                IsConfigured = true;
                return Task.CompletedTask;
            },
        };
    }

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;
}
