namespace NftFaucetRadzen.Plugins.NetworkPlugins.BinanceSmartChain.Networks;

public class BscMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("4a948c48-93cb-4b99-b11f-ef906f2751e1");
    public string Name { get; } = "Binance Smart Chain";
    public string ShortName { get; } = "BSC";
    public ulong? ChainId { get; } = 56;
    public int? Order { get; } = 1;
    public string Currency { get; } = "BNB";
    public string ImageName { get; } = "bnb.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Bsc;
    public Uri PublicRpcUrl { get; } = null;
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
