using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.ProviderPlugins.Phantom;

public class PhantomProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new PhantomProvider(),
    };
}
