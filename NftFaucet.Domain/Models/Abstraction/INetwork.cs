using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.Domain.Models.Abstraction;

public interface INetwork
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public ulong? ChainId { get; }
    public int? Order { get; }
    public string Currency { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsTestnet { get; }
    public bool IsDeprecated { get; }
    public NetworkType Type { get; }
    public NetworkSubtype SubType { get; }
    public Uri PublicRpcUrl { get; }
    public Uri ExplorerUrl { get; }
    public IReadOnlyCollection<IContract> DeployedContracts { get; }
}
