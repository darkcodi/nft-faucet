using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public sealed class SepoliaNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("3e792942-4f46-40a9-b2c7-e44a975678bc");
    public override string Name { get; } = "Sepolia";
    public override string ShortName { get; } = "Sepolia";
    public override ulong? ChainId { get; } = 11155111;
    public override int? Order { get; } = 7;
    public override string Currency { get; } = "SEP";
    public override string ImageName { get; } = "ethereum-gray.svg";
    public override bool IsSupported { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://sepolia.etherscan.io/");
}
