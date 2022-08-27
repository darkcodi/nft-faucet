namespace NftFaucetRadzen.Plugins.NetworkPlugins.Solana.Networks;

public class SolanaTestnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("12d13a34-689c-4fb1-84c0-7fcb719ef5b0");
    public string Name { get; } = "Solana Testnet";
    public string ShortName { get; } = "SolTestnet";
    public ulong? ChainId { get; } = null;
    public string Currency { get; } = "SOL";
    public string ImageName { get; } = "solana-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Solana;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
