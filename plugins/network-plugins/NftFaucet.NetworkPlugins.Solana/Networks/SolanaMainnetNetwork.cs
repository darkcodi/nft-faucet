using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.Solana.Networks;

public sealed class SolanaMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("1198f92d-3222-41e8-94af-8a7112324311");
    public override string Name { get; } = "Solana Mainnet";
    public override string ShortName { get; } = "Solana";
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "SOL";
    public override string ImageName { get; } = "solana.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Solana;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Solana;
    public override Uri PublicRpcUrl { get; } = new Uri("https://api.mainnet-beta.solana.com");
    public override Uri ExplorerUrl { get; } = new Uri("https://explorer.solana.com");
}
