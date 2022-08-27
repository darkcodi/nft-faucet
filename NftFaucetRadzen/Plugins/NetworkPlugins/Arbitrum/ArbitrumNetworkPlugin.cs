using NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum;

public class ArbitrumNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new ArbitrumOneNetwork(),
        new ArbitrumNovaNetwork(),
        new ArbitrumRinkebyNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
