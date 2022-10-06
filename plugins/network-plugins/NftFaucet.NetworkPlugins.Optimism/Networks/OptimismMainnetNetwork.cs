using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Optimism.Networks;

public sealed class OptimismMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("2fa5b635-6711-4994-9d2a-3ca730176516");
    public override string Name { get; } = "Optimism";
    public override string ShortName { get; } = "Optimism";
    public override ulong? ChainId { get; } = 10;
    public override int? Order { get; } = 1;
    public override string MainCurrency { get; } = "ETH";
    public override string SmallestCurrency { get; } = "wei";
    public override string ImageName { get; } = "optimism.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Optimism;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://optimistic.etherscan.io/");
}
