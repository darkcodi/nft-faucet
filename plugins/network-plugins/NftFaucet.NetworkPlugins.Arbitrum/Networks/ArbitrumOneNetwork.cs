using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Arbitrum.Networks;

public sealed class ArbitrumOneNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("4f0be8b9-dda1-4598-88b9-d4ba77f4c30e");
    public override string Name { get; } = "Arbitrum One";
    public override string ShortName { get; } = "Arbitrum";
    public override ulong? ChainId { get; } = 42161;
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "arbitrum.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://arbiscan.io/");
}
