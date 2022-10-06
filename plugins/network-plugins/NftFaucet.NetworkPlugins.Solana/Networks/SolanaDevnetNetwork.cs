using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.NetworkPlugins.Solana.Networks;

public sealed class SolanaDevnetNetwork : SolanaNetwork
{
    public override Guid Id { get; } = Guid.Parse("da9f269a-b53e-492a-be07-b4aadc2aae83");
    public override string Name { get; } = "Solana Devnet";
    public override string ShortName { get; } = "SolDevnet";
    public override int? Order { get; } = 2;
    public override string ImageName { get; } = "solana-black.svg";
    public override Uri PublicRpcUrl { get; } = new Uri("https://api.devnet.solana.com");
    public override Uri ExplorerUrl { get; } = new Uri("https://explorer.solana.com/?cluster=devnet");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("93c1f27e-1778-4b09-b81b-64491868a983"),
            Name = "Token Program",
            Symbol = "SPL",
            Address = "TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA",
            Type = ContractType.Solana,
            DeploymentTxHash = null,
            DeployedAt = null,
            IsVerified = true,
            MinBalanceRequired = MintingCost,
        },
    };
}
