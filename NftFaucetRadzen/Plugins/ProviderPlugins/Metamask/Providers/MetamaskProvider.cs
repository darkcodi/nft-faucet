using CSharpFunctionalExtensions;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Metamask.Providers;

public class MetamaskProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987");
    public string Name { get; } = "Metamask";
    public string ShortName { get; } = "Metamask";
    public string ImageName { get; } = "metamask_fox.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured { get; private set; }

    public CardListItemProperty[] GetProperties()
        => new CardListItemProperty[]
        {
            new CardListItemProperty{ Name = "Installed", Value = "YES" },
            new CardListItemProperty{ Name = "Connected", Value = IsConfigured ? "YES" : "NO" },
        };

    public CardListItemConfiguration GetConfiguration()
        => new CardListItemConfiguration
        {
            Objects = new[]
            {
                new CardListItemConfigurationObject
                {
                    Type = CardListItemConfigurationObjectType.Button,
                    Name = "Connect",
                    ClickAction = () => { },
                }
            },
            ConfigureAction = objects =>
            {
                IsConfigured = true;
                return Task.FromResult(Result.Success());
            },
        };

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;
}
