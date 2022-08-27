using NftFaucetRadzen.Plugins.NetworkPlugins.BinanceSmartChain.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.BinanceSmartChain;

public class BscNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new BscMainnetNetwork(),
        new BscTestnetNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
