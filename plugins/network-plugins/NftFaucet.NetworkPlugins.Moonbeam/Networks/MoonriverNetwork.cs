using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Moonbeam.Networks;

public sealed class MoonriverNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("51152b05-ebc6-46a2-9cab-10fb65f3e36b");
    public override string Name { get; } = "Moonriver";
    public override string ShortName { get; } = "Moonriver";
    public override ulong? ChainId { get; } = 1285;
    public override int? Order { get; } = 2;
    public override string Currency { get; } = "MOVR";
    public override string ImageName { get; } = "moonriver.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://blockscout.moonriver.moonbeam.network/");
}
