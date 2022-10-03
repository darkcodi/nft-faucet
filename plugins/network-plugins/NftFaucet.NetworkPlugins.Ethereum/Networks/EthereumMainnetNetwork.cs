using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class EthereumMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("a583d25d-883b-4f3c-9df1-6efe799c8fc4");
    public override string Name { get; } = "Mainnet";
    public override string ShortName { get; } = "Mainnet";
    public override ulong? ChainId { get; } = 1;
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "ethereum.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://etherscan.io/");
}
