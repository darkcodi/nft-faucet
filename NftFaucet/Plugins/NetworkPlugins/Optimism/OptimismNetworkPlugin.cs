using NftFaucet.Plugins.NetworkPlugins.Optimism.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.Optimism;

public class OptimismNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new OptimismMainnetNetwork(),
        new OptimismKovanNetwork(),
        new OptimismGoerliNetwork(),
    };
}
