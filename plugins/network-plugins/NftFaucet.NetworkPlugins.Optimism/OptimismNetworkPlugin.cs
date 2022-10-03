using NftFaucet.NetworkPlugins.Optimism.Networks;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Optimism;

public class OptimismNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new OptimismMainnetNetwork(),
        new OptimismKovanNetwork(),
        new OptimismGoerliNetwork(),
    };
}
