using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class CustomNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("28856b4c-d2d5-4b10-942d-f954f60150e0");
    public override string Name { get; } = "Custom";
    public override string ShortName { get; } = "custom";
    public override int? Order { get; } = 8;
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override bool IsSupported { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = null;
}
