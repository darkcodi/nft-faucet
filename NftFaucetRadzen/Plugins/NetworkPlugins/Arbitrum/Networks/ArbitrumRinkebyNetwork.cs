namespace NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum.Networks;

public class ArbitrumRinkebyNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("8189f9cd-14fc-41ab-9418-ca472ab15873");
    public string Name { get; } = "Arbitrum Rinkeby";
    public string ShortName { get; } = "ArbRinkeby";
    public ulong? ChainId { get; } = 421611;
    public int? Order { get; } = 3;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "arbitrum-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public string Erc721ContractAddress { get; } = "0x9F64932Be34D5D897C4253D17707b50921f372B6";
    public string Erc1155ContractAddress { get; } = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD";
}
