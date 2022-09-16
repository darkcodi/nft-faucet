using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Phantom.Providers;

public class PhantomProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("ae860901-5441-463c-a16e-4786f041500d");
    public string Name { get; } = "Phantom";
    public string ShortName { get; } = "Phantom";
    public string ImageName { get; } = "phantom.svg";
    public bool IsSupported { get; } = true;
    public bool CanBeConfigured { get; } = true;
    public bool IsConfigured { get; private set; }

    public Task Configure(CardListItemConfigurationObject[] items)
    {
        IsConfigured = true;
        return Task.CompletedTask;
    }

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
                    Id = Guid.Parse("4b10726b-eaac-48e6-9182-503db460d914"),
                    Type = CardListItemConfigurationObjectType.Button,
                    Name = "Connect",
                    ClickAction = () => { },
                }
            }
        };

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;
}
