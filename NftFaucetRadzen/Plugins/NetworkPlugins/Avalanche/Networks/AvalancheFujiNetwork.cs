namespace NftFaucetRadzen.Plugins.NetworkPlugins.Avalanche.Networks;

public class AvalancheFujiNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("2b809e32-739e-4dd0-9b48-bf83a4c3dfc5");
    public string Name { get; } = "Avalanche Fuji Testnet";
    public string ShortName { get; } = "Fuji";
    public ulong? ChainId { get; } = 43113;
    public string Currency { get; } = "AVAX";
    public string ImageName { get; } = "avalanche-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Avalanche;
    public string Erc721ContractAddress { get; } = "0x9F64932Be34D5D897C4253D17707b50921f372B6";
    public string Erc1155ContractAddress { get; } = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD";
}
