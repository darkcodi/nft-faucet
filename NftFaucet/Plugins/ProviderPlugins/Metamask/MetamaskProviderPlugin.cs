using NftFaucet.Plugins.ProviderPlugins.Metamask.Providers;

namespace NftFaucet.Plugins.ProviderPlugins.Metamask;

public class MetamaskProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new MetamaskProvider(),
    };
}
