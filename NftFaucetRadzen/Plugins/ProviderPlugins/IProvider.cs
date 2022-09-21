using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins;

public interface IProvider
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsConfigured { get; }

    public Task InitializeAsync(IServiceProvider serviceProvider);
    public CardListItemProperty[] GetProperties();
    public CardListItemConfiguration GetConfiguration();
    public bool IsNetworkSupported(INetwork network);
    public Task<string> GetAddress();
}
