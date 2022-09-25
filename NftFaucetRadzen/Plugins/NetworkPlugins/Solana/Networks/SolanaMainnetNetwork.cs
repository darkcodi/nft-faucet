namespace NftFaucetRadzen.Plugins.NetworkPlugins.Solana.Networks;

public class SolanaMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("1198f92d-3222-41e8-94af-8a7112324311");
    public string Name { get; } = "Solana Mainnet";
    public string ShortName { get; } = "Solana";
    public ulong? ChainId { get; } = null;
    public int? Order { get; } = 1;
    public string Currency { get; } = "SOL";
    public string ImageName { get; } = "solana.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Solana;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Solana;
    public Uri PublicRpcUrl { get; } = new Uri("https://api.mainnet-beta.solana.com");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
