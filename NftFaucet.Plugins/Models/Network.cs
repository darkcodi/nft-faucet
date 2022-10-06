using CSharpFunctionalExtensions;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins.Models;

public abstract class Network : DefaultEntity, INetwork
{
    public virtual ulong? ChainId { get; } = null;
    public virtual string Currency { get; } = null;
    public virtual bool IsTestnet { get; } = true;
    public abstract NetworkType Type { get; }
    public abstract NetworkSubtype SubType { get; }
    public abstract Uri PublicRpcUrl { get; }
    public abstract Uri ExplorerUrl { get; }
    public virtual IReadOnlyCollection<IContract> DeployedContracts { get; } = new List<IContract>();
    public virtual bool SupportsAirdrop { get; } = false;
    public virtual Task<Result> Airdrop(string address) => Task.FromResult(Result.Failure("Airdrop is not supported in this network"));
}
