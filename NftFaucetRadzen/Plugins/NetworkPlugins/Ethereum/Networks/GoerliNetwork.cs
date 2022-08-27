namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class GoerliNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("ac7858ff-b5c7-44f9-bf60-d81470531e56");
    public string Name { get; } = "Goerli";
    public string ShortName { get; } = "Goerli";
    public ulong? ChainId { get; } = 5;
    public int? Order { get; } = 4;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public string Erc721ContractAddress { get; } = "0xC3E4214dd442136079dF06bb2529Bae276d37564";
    public string Erc1155ContractAddress { get; } = "0x5807d7be82153F6a302d92199221090E3b78A3C3";
}
