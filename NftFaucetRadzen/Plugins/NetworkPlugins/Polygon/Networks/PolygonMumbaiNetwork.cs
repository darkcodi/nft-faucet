using System.Globalization;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Polygon.Networks;

public class PolygonMumbaiNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("c8f8b235-fde8-49f1-94a9-ab12a1188804");
    public string Name { get; } = "Polygon Mumbai";
    public string ShortName { get; } = "Mumbai";
    public ulong? ChainId { get; } = 80001;
    public int? Order { get; } = 2;
    public string Currency { get; } = "MATIC";
    public string ImageName { get; } = "polygon-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Polygon;
    public Uri PublicRpcUrl { get; } = new Uri("https://rpc-mumbai.maticvigil.com");
    public Uri ExplorerUrl { get; } = new Uri("https://mumbai.polygonscan.com/");

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("a7d0b843-d6c0-4e36-b57e-157b0c82c52b"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0xeE8272220A0988279627714144Ff6981E204fbE4",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0xd12be7bb46208c61fc71774f2d1934abea7c4f159196ddbcb6ceb26b703a614f",
            DeployedAt = DateTime.Parse("Apr-16-2022 06:14:40 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("a441cefd-35ae-458a-a96a-15c26895e5dc"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0x23147CdbD963A3D0fec0F25E4604844f477F65d2",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x996728d89d222c3af346d0e48c68d4ec9e5d73fa10c98f28ddfcd09b1a28ea59",
            DeployedAt = DateTime.Parse("Apr-17-2022 09:29:16 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
