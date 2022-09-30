using NftFaucet.Plugins.NetworkPlugins.BinanceSmartChain.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.BinanceSmartChain;

public class BscNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new BscMainnetNetwork(),
        new BscTestnetNetwork(),
    };
}
