using System.Globalization;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam.Networks;

public class MoonbaseAlphaNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("3232de5b-78bd-4b2f-8048-4aa3e16547bd");
    public string Name { get; } = "Moonbase Alpha";
    public string ShortName { get; } = "MoonAlpha";
    public ulong? ChainId { get; } = 1287;
    public int? Order { get; } = 3;
    public string Currency { get; } = "DEV";
    public string ImageName { get; } = "moonbeam-black.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Moonbase;

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("1d7f52d6-4c96-4b92-af4c-1e6485af9d2f"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x9F64932Be34D5D897C4253D17707b50921f372B6",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x9bb9cd82a83a708f395cf074ded75264c4fd31f6eeb64729b4fff1eeea2c5c08",
            DeployedAt = DateTime.Parse("Apr-17-2022 02:59:00 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("9d1b343b-248a-4a22-9543-b114dbb7ae67"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xf67C575502fc1cE399a3e1895dDf41847185D7bD",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x5f6d1137b59dbb0c00655a2c798b66ef34e42844dd89bbe45eb76b37fa82f718",
            DeployedAt = DateTime.Parse("Apr-17-2022 03:01:36 PM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
