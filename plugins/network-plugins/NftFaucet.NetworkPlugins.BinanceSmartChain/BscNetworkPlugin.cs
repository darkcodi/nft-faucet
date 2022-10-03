using NftFaucet.NetworkPlugins.BinanceSmartChain.Networks;
using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.BinanceSmartChain;

public class BscNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new BscMainnetNetwork(),
        new BscTestnetNetwork(),
    };
}
