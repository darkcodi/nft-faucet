namespace NftFaucetRadzen.Plugins.NetworkPlugins.Optimism.Networks;

public class OptimismKovanNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("a3d39e07-2fdd-450f-be3f-cdc8fcff676d");
    public string Name { get; } = "Optimism Kovan";
    public string ShortName { get; } = "OpKovan";
    public ulong? ChainId { get; } = 69;
    public int? Order { get; } = 2;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "optimism-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Optimism;
    public string Erc721ContractAddress { get; } = "0xee52f32f4bbcedc2a1ed1c195936132937f2d371";
    public string Erc1155ContractAddress { get; } = "0xCc0040129f197F63D37ebd77E62a6F96dDcd4e0A";
}
