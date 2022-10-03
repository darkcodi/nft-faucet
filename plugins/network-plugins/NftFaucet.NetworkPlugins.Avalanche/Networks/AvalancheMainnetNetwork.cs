using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Avalanche.Networks;

public sealed class AvalancheMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("35fba12e-aa79-4d7f-84bc-c120ca7d36a5");
    public override string Name { get; } = "Avalanche C-Chain";
    public override string ShortName { get; } = "Avalanche";
    public override ulong? ChainId { get; } = 43114;
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "AVAX";
    public override string ImageName { get; } = "avalanche.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Avalanche;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://snowtrace.io/");
}
