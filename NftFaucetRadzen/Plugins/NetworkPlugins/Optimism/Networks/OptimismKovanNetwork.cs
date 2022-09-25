using System.Globalization;

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
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Optimism;
    public Uri PublicRpcUrl { get; } = new Uri("https://mainnet.optimism.io");

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("c615845f-08e5-473e-b536-bd3da5447e06"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0xee52f32f4bbcedc2a1ed1c195936132937f2d371",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x4918d73cafaf7044fa580a50cc327db65628ec218cfcea891183842e67110f18",
            DeployedAt = DateTime.Parse("Apr-17-2022 12:51:54 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("161dd33f-dcf2-462d-aa81-5b14c7d4b5cb"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xCc0040129f197F63D37ebd77E62a6F96dDcd4e0A",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x3ead90561a03152c65b7ed8c3d208e7796b9d9a7b415582cdfe44ad40d9eb89e",
            DeployedAt = DateTime.Parse("Apr-17-2022 01:00:41 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
