using NftFaucetRadzen.Plugins.ProviderPlugins.Metamask.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Metamask;

public class MetamaskProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new MetamaskProvider(),
    };
}
