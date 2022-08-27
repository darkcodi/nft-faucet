namespace NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam.Networks;

public class MoonriverNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("51152b05-ebc6-46a2-9cab-10fb65f3e36b");
    public string Name { get; } = "Moonriver";
    public string ShortName { get; } = "Moonriver";
    public ulong? ChainId { get; } = 1285;
    public int? Order { get; } = 2;
    public string Currency { get; } = "MOVR";
    public string ImageName { get; } = "moonriver.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}