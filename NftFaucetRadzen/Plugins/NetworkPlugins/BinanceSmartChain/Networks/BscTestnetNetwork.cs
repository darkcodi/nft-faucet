namespace NftFaucetRadzen.Plugins.NetworkPlugins.BinanceSmartChain.Networks;

public class BscTestnetNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("b8d4aa13-acc1-47ee-9e8c-c00f0b67772c");
    public string Name { get; } = "Binance Smart Chain Testnet";
    public string ShortName { get; } = "BSC test";
    public ulong? ChainId { get; } = 97;
    public int? Order { get; } = 2;
    public string Currency { get; } = "tBNB";
    public string ImageName { get; } = "bnb-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Bsc;
    public string Erc721ContractAddress { get; } = "0xe6ee919a81da4dad1e632614ba4fdb8d748eb278";
    public string Erc1155ContractAddress { get; } = "0xa6d787d1ec987a96ba2a8bf4dae79234e4a2125a";
}
