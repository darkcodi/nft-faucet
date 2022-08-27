using NftFaucetRadzen.Plugins.NetworkPlugins.Optimism.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Optimism;

public class OptimismNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new OptimismMainnetNetwork(),
        new OptimismKovanNetwork(),
        new OptimismGoerliNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
