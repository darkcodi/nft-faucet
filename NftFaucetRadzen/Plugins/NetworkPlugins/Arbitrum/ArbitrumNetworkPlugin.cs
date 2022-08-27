using NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum;

public class ArbitrumNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new ArbitrumOneNetwork(),
        new ArbitrumNovaNetwork(),
        new ArbitrumRinkebyNetwork(),
        new ArbitrumGoerliNetwork(),
    };
}
