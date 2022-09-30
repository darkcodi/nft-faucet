namespace NftFaucet.Plugins.NetworkPlugins.Ethereum.Networks;

public class KilnNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("f2a3b097-c376-4608-9c20-1ad79cbf2d4f");
    public string Name { get; } = "Kiln";
    public string ShortName { get; } = "Kiln";
    public ulong? ChainId { get; } = 1337802;
    public int? Order { get; } = 6;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = null;
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
