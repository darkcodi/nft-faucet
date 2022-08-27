namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class RinkebyNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("795e27ea-942f-45e0-a5c8-b6c6a722635b");
    public string Name { get; } = "Rinkeby";
    public string ShortName { get; } = "Rinkeby";
    public ulong? ChainId { get; } = 4;
    public int? Order { get; } = 3;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public string Erc721ContractAddress { get; } = "0x9F64932Be34D5D897C4253D17707b50921f372B6";
    public string Erc1155ContractAddress { get; } = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD";
}
