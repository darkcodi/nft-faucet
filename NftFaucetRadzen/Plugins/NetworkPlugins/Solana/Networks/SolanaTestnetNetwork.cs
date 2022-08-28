namespace NftFaucetRadzen.Plugins.NetworkPlugins.Solana.Networks;

public class SolanaTestnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("12d13a34-689c-4fb1-84c0-7fcb719ef5b0");
    public string Name { get; } = "Solana Testnet";
    public string ShortName { get; } = "SolTestnet";
    public ulong? ChainId { get; } = null;
    public int? Order { get; } = 3;
    public string Currency { get; } = "SOL";
    public string ImageName { get; } = "solana-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Solana;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Solana;

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("794c6238-2d9a-4932-9c5a-d79edc783d47"),
            Name = "Token Program",
            Symbol = "SPL",
            Address = "TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA",
            Type = ContractType.Solana,
            DeploymentTxHash = "<unknown>",
            DeployedAt = DateTime.UnixEpoch,
            IsVerified = true,
        },
    };
}
