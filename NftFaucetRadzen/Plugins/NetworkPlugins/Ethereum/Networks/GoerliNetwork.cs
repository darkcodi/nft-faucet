using System.Globalization;

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
    public Uri PublicRpcUrl { get; } = new Uri("https://ethereum-goerli-rpc.allthatnode.com");

    public IReadOnlyCollection<IContract> DeployedContracts { get; } = new[]
    {
        new Contract
        {
            Id = Guid.Parse("ac45dd53-2126-4cbd-9cec-59e4374aad98"),
            Name = "ERC-721 Faucet",
            Symbol = "FA721",
            Address = "0xC3E4214dd442136079dF06bb2529Bae276d37564",
            Type = ContractType.Erc721,
            DeploymentTxHash = "0xb117ee2482ae4eb1a95e1543d06c89e65aab4ef1ed73f32d20789bf1a88abfac",
            DeployedAt = DateTime.Parse("Apr-17-2022 10:50:21 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
        new Contract
        {
            Id = Guid.Parse("ef22410c-a8e0-45ca-9e9c-c10629802c69"),
            Name = "ERC-1155 Faucet",
            Symbol = "FA1155",
            Address = "0x5807d7be82153F6a302d92199221090E3b78A3C3",
            Type = ContractType.Erc1155,
            DeploymentTxHash = "0x00983c36d7cd73d63471d730a2260a022a45ac3371b5342eded8b2e31ae11a2e",
            DeployedAt = DateTime.Parse("Apr-17-2022 10:54:21 AM", CultureInfo.InvariantCulture),
            IsVerified = true,
        },
    };
}
