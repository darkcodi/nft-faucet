using NftFaucet.Plugins.ProviderPlugins.Keygen.Providers;

namespace NftFaucet.Plugins.ProviderPlugins.Keygen;

public class KeygenProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new EthereumKeygenProvider(),
        new SolanaKeygenProvider(),
    };
}
