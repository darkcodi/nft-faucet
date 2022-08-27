namespace NftFaucetRadzen.Plugins.NetworkPlugins.Optimism.Networks;

public class OptimismMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("2fa5b635-6711-4994-9d2a-3ca730176516");
    public string Name { get; } = "Optimism";
    public string ShortName { get; } = "Optimism";
    public ulong? ChainId { get; } = 10;
    public int? Order { get; } = 1;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "optimism.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Optimism;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
