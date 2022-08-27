namespace NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam.Networks;

public class MoonbeamNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("04b52a79-14d7-403c-9411-2af240fa7984");
    public string Name { get; } = "Moonbeam";
    public string ShortName { get; } = "Moonbeam";
    public ulong? ChainId { get; } = 1284;
    public int? Order { get; } = 1;
    public string Currency { get; } = "GLMR";
    public string ImageName { get; } = "moonbeam.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = false;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Moonbase;
    public string Erc721ContractAddress { get; } = null;
    public string Erc1155ContractAddress { get; } = null;
}
