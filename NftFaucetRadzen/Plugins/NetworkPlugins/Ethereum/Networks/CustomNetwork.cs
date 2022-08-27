namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class CustomNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("28856b4c-d2d5-4b10-942d-f954f60150e0");
    public string Name { get; } = "Custom";
    public string ShortName { get; } = "custom";
    public ulong? ChainId { get; } = null;
    public int? Order { get; } = 8;
    public string Currency { get; } = null;
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
