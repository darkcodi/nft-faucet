using NftFaucet.Plugins.NetworkPlugins.Avalanche.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.Avalanche;

public class AvalancheNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new AvalancheMainnetNetwork(),
        new AvalancheFujiNetwork(),
    };
}
