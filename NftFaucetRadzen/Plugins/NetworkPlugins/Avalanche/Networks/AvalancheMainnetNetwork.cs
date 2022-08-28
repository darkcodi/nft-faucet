namespace NftFaucetRadzen.Plugins.NetworkPlugins.Avalanche.Networks;

public class AvalancheMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("35fba12e-aa79-4d7f-84bc-c120ca7d36a5");
    public string Name { get; } = "Avalanche C-Chain";
    public string ShortName { get; } = "Avalanche";
    public ulong? ChainId { get; } = 43114;
    public int? Order { get; } = 1;
    public string Currency { get; } = "AVAX";
    public string ImageName { get; } = "avalanche.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Avalanche;
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
