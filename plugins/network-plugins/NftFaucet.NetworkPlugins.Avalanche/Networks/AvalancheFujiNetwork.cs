using System.Globalization;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.NetworkPlugins.Avalanche.Networks;

public class AvalancheFujiNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("2b809e32-739e-4dd0-9b48-bf83a4c3dfc5");
    public string Name { get; } = "Avalanche Fuji Testnet";
    public string ShortName { get; } = "Fuji";
    public ulong? ChainId { get; } = 43113;
    public int? Order { get; } = 2;
    public string Currency { get; } = "AVAX";
    public string ImageName { get; } = "avalanche-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Avalanche;
    public Uri PublicRpcUrl { get; } = new Uri("https://api.avax-test.network/ext/bc/C/rpc");
    public Uri ExplorerUrl { get; } = new Uri("https://testnet.snowtrace.io/");

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("aa746e1f-1d9f-43e6-91b3-3c68740bb19f"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x9d42fec82660afe1e175384d0187a40e6a5d6f48365f6245d42f97d6886e5721",
            DeployedAt = DateTime.Parse("Apr-17-2022 02:27:47 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("670ae59c-8ba7-48c7-b16f-ac312efbbba8"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x7f5969597e135e45e8f560e622fffafb0280c871d7a0530b8a0d0dcaef1be6b0",
            DeployedAt = DateTime.Parse("Apr-17-2022 02:30:57 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
