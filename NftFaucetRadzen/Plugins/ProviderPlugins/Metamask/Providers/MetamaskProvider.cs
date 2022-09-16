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
    public bool CanBeConfigured { get; } = true;
    public bool IsConfigured { get; private set; }

    public Task Configure(CardListItemConfigurationObject[] items)
    {
        IsConfigured = true;
        return Task.CompletedTask;
    }

    public List<(string Name, string Value)> GetProperties()
        => new List<(string Name, string Value)>
        {
            ("Installed", "YES"),
            ("Connected", IsConfigured ? "YES" : "NO"),
        };

    public CardListItemConfiguration GetConfiguration()
        => new CardListItemConfiguration
        {
            Objects = new[]
            {
                new CardListItemConfigurationObject
                {
                    Id = Guid.Parse("88ad581f-17a4-452a-9e21-b413a2887955"),
                    Type = CardListItemConfigurationObjectType.Button,
                    Name = "Connect",
                    ClickAction = () => { },
                }
            }
        };

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;
}
