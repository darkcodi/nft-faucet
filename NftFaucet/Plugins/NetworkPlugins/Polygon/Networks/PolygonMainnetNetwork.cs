namespace NftFaucet.Plugins.NetworkPlugins.Polygon.Networks;

public class PolygonMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("e09e7646-cf39-42be-8c8d-c566442e8229");
    public string Name { get; } = "Polygon";
    public string ShortName { get; } = "Polygon";
    public ulong? ChainId { get; } = 137;
    public int? Order { get; } = 1;
    public string Currency { get; } = "MATIC";
    public string ImageName { get; } = "polygon.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Polygon;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = new Uri("https://polygonscan.com/");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
