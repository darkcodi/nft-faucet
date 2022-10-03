using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class KilnNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("f2a3b097-c376-4608-9c20-1ad79cbf2d4f");
    public override string Name { get; } = "Kiln";
    public override string ShortName { get; } = "Kiln";
    public override ulong? ChainId { get; } = 1337802;
    public override int? Order { get; } = 6;
    public override string Currency { get; } = "ETH";
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsDeprecated { get; } = true;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = null;
}
