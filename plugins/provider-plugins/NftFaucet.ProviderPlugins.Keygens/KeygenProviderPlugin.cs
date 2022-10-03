using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.ProviderPlugins.EthereumKeygen;

namespace NftFaucet.ProviderPlugins.Keygens;

public class KeygenProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new EthereumKeygenProvider(),
        new SolanaKeygenProvider(),
    };
}
