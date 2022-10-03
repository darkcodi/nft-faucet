using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Optimism.Networks;

public sealed class OptimismKovanNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("a3d39e07-2fdd-450f-be3f-cdc8fcff676d");
    public override string Name { get; } = "Optimism Kovan";
    public override string ShortName { get; } = "OpKovan";
    public override ulong? ChainId { get; } = 69;
    public override int? Order { get; } = 2;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "optimism-black.svg";
    public override bool IsDeprecated { get; } = true;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Optimism;
    public override Uri PublicRpcUrl { get; } = new Uri("https://mainnet.optimism.io");
    public override Uri ExplorerUrl { get; } = new Uri("https://kovan-optimistic.etherscan.io/");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
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
