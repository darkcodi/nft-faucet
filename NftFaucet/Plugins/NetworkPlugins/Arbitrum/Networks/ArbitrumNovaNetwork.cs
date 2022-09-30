namespace NftFaucet.Plugins.NetworkPlugins.Arbitrum.Networks;

public class ArbitrumNovaNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("e2f056f8-1c5c-494f-9e88-96213a2009d4");
    public string Name { get; } = "Arbitrum Nova";
    public string ShortName { get; } = "ArbNova";
    public ulong? ChainId { get; } = 42170;
    public int? Order { get; } = 2;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "arbitrum-black.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = new Uri("https://nova-explorer.arbitrum.io");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
