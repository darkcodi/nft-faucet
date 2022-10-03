using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;

namespace NftFaucet.NetworkPlugins.BinanceSmartChain.Networks;

public sealed class BscMainnetNetwork : Network
{
    public override Guid Id { get; } = Guid.Parse("4a948c48-93cb-4b99-b11f-ef906f2751e1");
    public override string Name { get; } = "Binance Smart Chain";
    public override string ShortName { get; } = "BSC";
    public override ulong? ChainId { get; } = 56;
    public override int? Order { get; } = 1;
    public override string Currency { get; } = "BNB";
    public override string ImageName { get; } = "bnb.svg";
    public override bool IsSupported { get; } = false;
    public override bool IsTestnet { get; } = false;
    public override NetworkType Type { get; } = NetworkType.Ethereum;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Bsc;
    public override Uri PublicRpcUrl { get; } = null;
    public override Uri ExplorerUrl { get; } = new Uri("https://bscscan.com/");
}
