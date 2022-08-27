using NftFaucetRadzen.Plugins.ProviderPlugins.Phantom.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Phantom;

public class PhantomProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new PhantomProvider(),
    };
}
