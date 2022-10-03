using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class RopstenNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("d26d00d8-5036-433d-a4bd-2383f3c4c47c");
    public override string Name { get; } = "Ropsten";
    public override string ShortName { get; } = "Ropsten";
    public override ulong? ChainId { get; } = 3;
    public override int? Order { get; } = 2;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override bool IsDeprecated { get; } = true;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = new Uri("https://ethereum-ropsten-rpc.allthatnode.com");
    public override Uri ExplorerUrl { get; } = new Uri("https://ropsten.etherscan.io/");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("da6a20fc-4629-40b1-98b7-a15732c487fe"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x71902F99902339d7ce1F994C12155f4350BCD226",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x6d17837aca212bc80302ab6b73bd6f39a8a168c588a5efc6eb10c0b7e89015cf",
            DeployedAt = DateTime.Parse("Apr-16-2022 06:07:30 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("2d60c703-048f-432b-8391-db5ba51a6ea3"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0x80b45421881c0452A6e70148Fc928fA33107cEb3",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0xaf4f4932aeedbaa2f3bf96bb091ba35a15ffc4951b45b1ec521b0c63e11a602a",
            DeployedAt = DateTime.Parse("Apr-17-2022 09:28:58 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
