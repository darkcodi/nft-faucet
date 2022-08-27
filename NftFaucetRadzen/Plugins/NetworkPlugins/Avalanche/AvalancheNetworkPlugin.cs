using NftFaucetRadzen.Plugins.NetworkPlugins.Avalanche.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Avalanche;

public class AvalancheNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new AvalancheMainnetNetwork(),
        new AvalancheFujiNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
