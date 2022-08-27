namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class KovanNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("2d76565d-6e66-4d5b-bd62-c44e4db95782");
    public string Name { get; } = "Kovan";
    public string ShortName { get; } = "Kovan";
    public ulong? ChainId { get; } = 42;
    public int? Order { get; } = 5;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public string Erc721ContractAddress { get; } = "0x99ea658e02baDE18c43Af5Fa8c18cfF4f251E311";
    public string Erc1155ContractAddress { get; } = "0xdBDD0377D1799910A4B0a4306F8d812265bF33Cb";
}
