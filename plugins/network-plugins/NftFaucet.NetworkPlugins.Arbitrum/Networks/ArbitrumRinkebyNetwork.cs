using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Arbitrum.Networks;

public sealed class ArbitrumRinkebyNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("8189f9cd-14fc-41ab-9418-ca472ab15873");
    public override string Name { get; } = "Arbitrum Rinkeby";
    public override string ShortName { get; } = "ArbRinkeby";
    public override ulong? ChainId { get; } = 421611;
    public override int? Order { get; } = 3;
    public override string MainCurrency { get; } = "ETH";
    public override string SmallestCurrency { get; } = "wei";
    public override string ImageName { get; } = "arbitrum-black.svg";
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public override Uri PublicRpcUrl { get; } = new Uri("https://rinkeby.arbitrum.io/rpc");
    public override Uri ExplorerUrl { get; } = new Uri("https://rinkeby-explorer.arbitrum.io/");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("f82fc396-1cfd-4db7-af27-9e3e7d4264f6"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x506e9c08cb360eb85b0b5d1e86615178ff1e4c0fab05297e725e9d227f45fe6b",
            DeployedAt = DateTime.Parse("Apr-17-2022 01:45:55 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
            MinBalanceRequired = 50000000000000,
        },
        new Contract
        {
            Id = Guid.Parse("6e125364-b8fd-49fe-b453-cfa69ebad811"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0xe62c31c70fd37a4b984b4b51d2ee5e08cd6bc53983200ca0041656f57f6dc8de",
            DeployedAt = DateTime.Parse("Apr-17-2022 01:49:41 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
            MinBalanceRequired = 50000000000000,
        },
    };
}
