namespace NftFaucetRadzen.Plugins.NetworkPlugins.Polygon.Networks;

public class PolygonMumbaiNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("c8f8b235-fde8-49f1-94a9-ab12a1188804");
    public string Name { get; } = "Polygon Mumbai";
    public string ShortName { get; } = "Mumbai";
    public ulong? ChainId { get; } = 80001;
    public string Currency { get; } = "MATIC";
    public string ImageName { get; } = "polygon-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Polygon;
    public string Erc721ContractAddress { get; } = "0xeE8272220A0988279627714144Ff6981E204fbE4";
    public string Erc1155ContractAddress { get; } = "0x23147CdbD963A3D0fec0F25E4604844f477F65d2";
}
