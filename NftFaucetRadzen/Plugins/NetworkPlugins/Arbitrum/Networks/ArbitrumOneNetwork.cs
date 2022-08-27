namespace NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum.Networks;

public class ArbitrumOneNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("4f0be8b9-dda1-4598-88b9-d4ba77f4c30e");
    public string Name { get; } = "Arbitrum One";
    public string ShortName { get; } = "Arbitrum";
    public ulong? ChainId { get; } = 42161;
    public int? Order { get; } = 1;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "arbitrum.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
