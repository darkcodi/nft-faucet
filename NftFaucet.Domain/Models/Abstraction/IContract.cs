using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.Domain.Models.Abstraction;

public interface IContract
{
    public Guid Id { get; }
    public string Name { get; }
    public string Symbol { get; }
    public string Address { get; }
    public ContractType Type { get; }
    public string DeploymentTxHash { get; }
    public DateTime? DeployedAt { get; }
    public bool IsVerified { get; }
    public ulong MinBalanceRequired { get; }
}
