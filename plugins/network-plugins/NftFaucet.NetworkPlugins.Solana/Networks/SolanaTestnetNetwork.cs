using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.NetworkPlugins.Solana.Networks;

public sealed class SolanaTestnetNetwork : SolanaNetwork
{
    public override Guid Id { get; } = Guid.Parse("12d13a34-689c-4fb1-84c0-7fcb719ef5b0");
    public override string Name { get; } = "Solana Testnet";
    public override string ShortName { get; } = "SolTestnet";
    public override int? Order { get; } = 3;
    public override string ImageName { get; } = "solana-black.svg";
    public override Uri PublicRpcUrl { get; } = new Uri("https://api.testnet.solana.com");
    public override Uri ExplorerUrl { get; } = new Uri("https://explorer.solana.com/?cluster=testnet");

    public override IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("794c6238-2d9a-4932-9c5a-d79edc783d47"),
            Name = "Token Program",
            Symbol = "SPL",
            Address = "TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA",
            Type = ContractType.Solana,
            DeploymentTxHash = null,
            DeployedAt = null,
            IsVerified = true,
        },
    };
}
