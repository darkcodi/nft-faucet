namespace NftFaucetRadzen.Plugins.NetworkPlugins.Solana.Networks;

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
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
