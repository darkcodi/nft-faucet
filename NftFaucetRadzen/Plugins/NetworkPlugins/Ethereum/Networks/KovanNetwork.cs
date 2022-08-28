using System.Globalization;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class KovanNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("2d76565d-6e66-4d5b-bd62-c44e4db95782");
    public string Name { get; } = "Kovan";
    public string ShortName { get; } = "Kovan";
    public ulong? ChainId { get; } = 42;
    public int? Order { get; } = 5;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("142b03fb-bc64-4db2-af0f-6b43f74b8db1"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0x99ea658e02baDE18c43Af5Fa8c18cfF4f251E311",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0x2e9e02701d8571d0af5910c25a16aca3520b5dd091507bde9347c5b2a760e8cb",
            DeployedAt = DateTime.Parse("Apr-17-2022 11:54:12 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("46e3f075-2c6e-4b54-bc5f-1f0a4f15379c"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0xdBDD0377D1799910A4B0a4306F8d812265bF33Cb",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0xa095f2a30576a8bb38a49774fadc79c65d798c46999c367a74faf281950ba327",
            DeployedAt = DateTime.Parse("Apr-17-2022 11:56:24 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
