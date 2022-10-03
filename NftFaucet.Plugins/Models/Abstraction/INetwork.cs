using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.Plugins.Models.Abstraction;

public interface INetwork : INamedEntity, IEntityWithOrder
{
    public ulong? ChainId { get; }
    public string Currency { get; }
    public bool IsTestnet { get; }
    public NetworkType Type { get; }
    public NetworkSubtype SubType { get; }
    public Uri PublicRpcUrl { get; }
    public Uri ExplorerUrl { get; }
    public IReadOnlyCollection<IContract> DeployedContracts { get; }
}
