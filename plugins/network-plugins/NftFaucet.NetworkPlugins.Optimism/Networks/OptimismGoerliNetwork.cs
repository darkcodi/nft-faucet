using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Optimism.Networks;

public sealed class OptimismGoerliNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("fe4f3f37-bec9-4f35-9063-8682160b1f9d");
    public override string Name { get; } = "Optimism Goerli";
    public override string ShortName { get; } = "OpGoerli";
    public override ulong? ChainId { get; } = 420;
    public override int? Order { get; } = 3;
    public override string MainCurrency { get; } = "ETH";
    public override string SmallestCurrency { get; } = "wei";
    public override string ImageName { get; } = "optimism-black.svg";
    public override bool IsSupported { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Optimism;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://blockscout.com/optimism/goerli/");
}
