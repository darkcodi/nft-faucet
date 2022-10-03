using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Arbitrum.Networks;

public sealed class ArbitrumNovaNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("e2f056f8-1c5c-494f-9e88-96213a2009d4");
    public override string Name { get; } = "Arbitrum Nova";
    public override string ShortName { get; } = "ArbNova";
    public override ulong? ChainId { get; } = 42170;
    public override int? Order { get; } = 2;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "arbitrum-black.svg";
    public override bool IsSupported { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://nova-explorer.arbitrum.io");
}
