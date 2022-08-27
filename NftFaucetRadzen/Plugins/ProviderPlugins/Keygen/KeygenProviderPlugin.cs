using NftFaucetRadzen.Plugins.ProviderPlugins.Keygen.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Keygen;

public class KeygenProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new EthereumKeygenProvider(),
    };
}
