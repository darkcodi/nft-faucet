using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;

namespace NftFaucet.NetworkPlugins.Ethereum.Networks;

public class SepoliaNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("3e792942-4f46-40a9-b2c7-e44a975678bc");
    public string Name { get; } = "Sepolia";
    public string ShortName { get; } = "Sepolia";
    public ulong? ChainId { get; } = 11155111;
    public int? Order { get; } = 7;
    public string Currency { get; } = "SEP";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = false;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = false;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public NetworkSubtype SubType { get; } = NetworkSubtype.Ethereum;
    public Uri PublicRpcUrl { get; } = null;
    public Uri ExplorerUrl { get; } = new Uri("https://sepolia.etherscan.io/");
    public IReadOnlyCollection<IContract> DeployedContracts { get; } = Array.Empty<IContract>();
}
