using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class RinkebyNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("795e27ea-942f-45e0-a5c8-b6c6a722635b");
    public override string Name { get; } = "Rinkeby";
    public override string ShortName { get; } = "Rinkeby";
    public override ulong? ChainId { get; } = 4;
    public override int? Order { get; } = 3;
    public override string MainCurrency { get; } = "ETH";
    public override string SmallestCurrency { get; } = "wei";
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override bool IsDeprecated { get; } = true;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = new Uri("https://ethereum-rinkeby-rpc.allthatnode.com");
    public override Uri ExplorerUrl { get; } = new Uri("https://rinkeby.etherscan.io/");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("c18c6b3f-c6c1-4aae-92b6-c5158c1704eb"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0xc1db534202f68a169a463b5053de7011330d870c9c63dd69bf03dd72c0d99f8b",
            DeployedAt = DateTime.Parse("Apr-17-2022 10:20:34 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
            MinBalanceRequired = 50000000000000,
        },
        new Contract
        {
            Id = Guid.Parse("8ca22018-337a-4485-bc9a-909ec438bead"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0xa216e02c0978b1dc2ff2d33b981345861136cbe611dfc5c53500fd16da81654b",
            DeployedAt = DateTime.Parse("Apr-17-2022 10:40:06 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
            MinBalanceRequired = 50000000000000,
        },
    };
}
