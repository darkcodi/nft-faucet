namespace NftFaucetRadzen.Plugins.NetworkPlugins.Optimism.Networks;

public class OptimismGoerliNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("fe4f3f37-bec9-4f35-9063-8682160b1f9d");
    public string Name { get; } = "Optimism Goerli";
    public string ShortName { get; } = "OpGoerli";
    public ulong? ChainId { get; } = 420;
    public int? Order { get; } = 3;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "optimism-black.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Optimism;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = new Uri("https://blockscout.com/optimism/goerli/");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
