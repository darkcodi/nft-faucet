namespace NftFaucetRadzen.Plugins.NetworkPlugins.Avalanche.Networks;

public class AvalancheMainnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("35fba12e-aa79-4d7f-84bc-c120ca7d36a5");
    public string Name { get; } = "Avalanche C-Chain";
    public string ShortName { get; } = "Avalanche";
    public ulong? ChainId { get; } = 43114;
    public string Currency { get; } = "AVAX";
    public string ImageName { get; } = "avalanche.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Avalanche;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
