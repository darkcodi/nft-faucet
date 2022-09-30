using NftFaucet.Plugins.NetworkPlugins.Arbitrum.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.Arbitrum;

public class ArbitrumNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new ArbitrumOneNetwork(),
        new ArbitrumNovaNetwork(),
        new ArbitrumRinkebyNetwork(),
    };
}
