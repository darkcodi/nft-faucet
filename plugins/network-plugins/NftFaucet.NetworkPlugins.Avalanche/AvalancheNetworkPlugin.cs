using NftFaucet.NetworkPlugins.Avalanche.Networks;
using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Avalanche;

public class AvalancheNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new AvalancheMainnetNetwork(),
        new AvalancheFujiNetwork(),
    };
}
