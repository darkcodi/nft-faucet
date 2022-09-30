using NftFaucet.Plugins.NetworkPlugins.Polygon.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.Polygon;

public class PolygonNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new PolygonMainnetNetwork(),
        new PolygonMumbaiNetwork(),
    };
}
