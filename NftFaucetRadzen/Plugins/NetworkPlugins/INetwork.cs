namespace NftFaucetRadzen.Plugins.NetworkPlugins;

public interface INetwork
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public ulong? ChainId { get; }
    public int? Order { get; }
    public string Currency { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsTestnet { get; }
    public bool IsDeprecated { get; }
    public NetworkType Type { get; }
    public NetworkSubtype SubType { get; }
    public string Erc721ContractAddress { get; }
    public string Erc1155ContractAddress { get; }
}
