using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.Domain.Models;

public class Contract : IContract
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Symbol { get; init; }
    public string Address { get; init; }
    public ContractType Type { get; init; }
    public string DeploymentTxHash { get; init; }
    public DateTime? DeployedAt { get; init; }
    public bool IsVerified { get; init; }
    public ulong MinBalanceRequired { get; init; }
}
