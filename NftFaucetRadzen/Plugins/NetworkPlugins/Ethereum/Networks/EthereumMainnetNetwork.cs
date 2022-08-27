namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class EthereumMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("a583d25d-883b-4f3c-9df1-6efe799c8fc4");
    public string Name { get; } = "Mainnet";
    public string ShortName { get; } = "Mainnet";
    public ulong? ChainId { get; } = 1;
    public int? Order { get; } = 1;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
