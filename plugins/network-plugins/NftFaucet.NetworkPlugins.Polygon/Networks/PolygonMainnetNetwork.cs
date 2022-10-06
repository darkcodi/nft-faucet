using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Polygon.Networks;

public sealed class PolygonMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("e09e7646-cf39-42be-8c8d-c566442e8229");
    public override string Name { get; } = "Polygon";
    public override string ShortName { get; } = "Polygon";
    public override ulong? ChainId { get; } = 137;
    public override int? Order { get; } = 1;
    public override string MainCurrency { get; } = "MATIC";
    public override string SmallestCurrency { get; } = "wei";
    public override string ImageName { get; } = "polygon.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Polygon;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://polygonscan.com/");
}
