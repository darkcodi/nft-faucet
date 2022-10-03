using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class SepoliaNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("3e792942-4f46-40a9-b2c7-e44a975678bc");
    public override string Name { get; } = "Sepolia";
    public override string ShortName { get; } = "Sepolia";
    public override ulong? ChainId { get; } = 11155111;
    public override int? Order { get; } = 7;
    public override string Currency { get; } = "SEP";
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = new Uri("https://rpc.sepolia.org");
    public override Uri ExplorerUrl { get; } = new Uri("https://sepolia.etherscan.io/");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("c00521a1-af2e-44c3-8af2-d83426f79d62"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0xfa17519ea84d95c346c6803d64ba71ab22455a0a9ffbe3803be6e7bd9cc8a371",
            DeployedAt = DateTime.Parse("Oct-03-2022 03:33:36 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("bd3bb27b-827c-4936-a7ed-f7e8449ba97a"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0x1eD60FedfF775D500DDe21A974cd4E92e0047Cc8",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x2991cffbd195514207b4d0880530690be351d2bdd03bb29d3442552225b0dd79",
            DeployedAt = DateTime.Parse("Oct-03-2022 03:36:36 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
