using System.Globalization;

namespace NftFaucet.Plugins.NetworkPlugins.Arbitrum.Networks;

public class ArbitrumRinkebyNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("8189f9cd-14fc-41ab-9418-ca472ab15873");
    public string Name { get; } = "Arbitrum Rinkeby";
    public string ShortName { get; } = "ArbRinkeby";
    public ulong? ChainId { get; } = 421611;
    public int? Order { get; } = 3;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "arbitrum-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Arbitrum;
    public Uri PublicRpcUrl { get; } = new Uri("https://rinkeby.arbitrum.io/rpc");
    public Uri ExplorerUrl { get; } = new Uri("https://rinkeby-explorer.arbitrum.io/");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("f82fc396-1cfd-4db7-af27-9e3e7d4264f6"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x506e9c08cb360eb85b0b5d1e86615178ff1e4c0fab05297e725e9d227f45fe6b",
            DeployedAt = DateTime.Parse("Apr-17-2022 01:45:55 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("6e125364-b8fd-49fe-b453-cfa69ebad811"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0xe62c31c70fd37a4b984b4b51d2ee5e08cd6bc53983200ca0041656f57f6dc8de",
            DeployedAt = DateTime.Parse("Apr-17-2022 01:49:41 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
