using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.NetworkPlugins.Moonbeam.Networks;

public class MoonbeamNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("04b52a79-14d7-403c-9411-2af240fa7984");
    public string Name { get; } = "Moonbeam";
    public string ShortName { get; } = "Moonbeam";
    public ulong? ChainId { get; } = 1284;
    public int? Order { get; } = 1;
    public string Currency { get; } = "GLMR";
    public string ImageName { get; } = "moonbeam.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = new Uri("https://blockscout.moonbeam.network/");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
