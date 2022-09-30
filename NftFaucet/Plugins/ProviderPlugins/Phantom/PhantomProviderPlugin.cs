using NftFaucet.Plugins.ProviderPlugins.Phantom.Providers;

namespace NftFaucet.Plugins.ProviderPlugins.Phantom;

public class PhantomProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new PhantomProvider(),
    };
}
