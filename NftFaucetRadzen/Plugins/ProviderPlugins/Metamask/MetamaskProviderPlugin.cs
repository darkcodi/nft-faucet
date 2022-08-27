using NftFaucetRadzen.Plugins.ProviderPlugins.Metamask.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Metamask;

public class MetamaskProviderPlugin : IProviderPlugin
{
    private IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new MetamaskProvider(),
    };

    public IReadOnlyCollection<IProvider> GetProviders() => Providers;
}
