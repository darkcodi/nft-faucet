namespace NftFaucet.Plugins.NetworkPlugins.Solana.Networks;

public class SolanaDevnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("da9f269a-b53e-492a-be07-b4aadc2aae83");
    public string Name { get; } = "Solana Devnet";
    public string ShortName { get; } = "SolDevnet";
    public ulong? ChainId { get; } = null;
    public int? Order { get; } = 2;
    public string Currency { get; } = "SOL";
    public string ImageName { get; } = "solana-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Solana;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Solana;
    public Uri PublicRpcUrl { get; } = new Uri("https://api.devnet.solana.com");
    public Uri ExplorerUrl { get; } = new Uri("https://explorer.solana.com/?cluster=devnet");

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
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
        },
    };
}
