using NftFaucet.NetworkPlugins.Arbitrum.Networks;
using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Arbitrum;

public class ArbitrumNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new ArbitrumOneNetwork(),
        new ArbitrumNovaNetwork(),
        new ArbitrumRinkebyNetwork(),
    };
}
