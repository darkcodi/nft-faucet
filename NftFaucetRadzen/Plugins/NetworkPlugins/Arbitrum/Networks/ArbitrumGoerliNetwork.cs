namespace NftFaucetRadzen.Plugins.NetworkPlugins.Arbitrum.Networks;

public class ArbitrumGoerliNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("0a46cc45-7b4a-4a43-b677-3cb5e4c7c512");
    public string Name { get; } = "Arbitrum Goerli";
    public string ShortName { get; } = "ArbGoerli";
    public ulong? ChainId { get; } = 421613;
    public int? Order { get; } = 4;
    public string Currency { get; } = "AGOR";
    public string ImageName { get; } = "arbitrum-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public string Erc721ContractAddress { get; } = "0x9F64932Be34D5D897C4253D17707b50921f372B6";
    public string Erc1155ContractAddress { get; } = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD";
}
