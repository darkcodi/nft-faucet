namespace NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam.Networks;

public class MoonbaseAlphaNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("3232de5b-78bd-4b2f-8048-4aa3e16547bd");
    public string Name { get; } = "Moonbase Alpha";
    public string ShortName { get; } = "MoonAlpha";
    public ulong? ChainId { get; } = 1287;
    public int? Order { get; } = 3;
    public string Currency { get; } = "DEV";
    public string ImageName { get; } = "moonbeam-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;
    public string Erc721ContractAddress { get; } = "0x9F64932Be34D5D897C4253D17707b50921f372B6";
    public string Erc1155ContractAddress { get; } = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD";
}
