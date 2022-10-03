using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Moonbeam.Networks;

public sealed class MoonbeamNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("04b52a79-14d7-403c-9411-2af240fa7984");
    public override string Name { get; } = "Moonbeam";
    public override string ShortName { get; } = "Moonbeam";
    public override ulong? ChainId { get; } = 1284;
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "GLMR";
    public override string ImageName { get; } = "moonbeam.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://blockscout.moonbeam.network/");
}
