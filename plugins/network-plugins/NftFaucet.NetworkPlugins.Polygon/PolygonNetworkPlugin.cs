using NftFaucet.NetworkPlugins.Polygon.Networks;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Polygon;

public class PolygonNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new PolygonMainnetNetwork(),
        new PolygonMumbaiNetwork(),
    };
}
