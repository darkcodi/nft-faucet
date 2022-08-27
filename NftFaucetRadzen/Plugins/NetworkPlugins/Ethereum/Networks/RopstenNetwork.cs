namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

public class RopstenNetwork : INetwork
{
    public Guid Id { get; } = Guid.Parse("d26d00d8-5036-433d-a4bd-2383f3c4c47c");
    public string Name { get; } = "Ropsten";
    public string ShortName { get; } = "Ropsten";
    public ulong? ChainId { get; } = 3;
    public int? Order { get; } = 2;
    public string Currency { get; } = "ETH";
    public string ImageName { get; } = "ethereum-gray.svg";
    public bool IsSupported { get; } = true;
    public bool IsTestnet { get; } = true;
    public bool IsDeprecated { get; } = true;
    public NetworkType Type { get; } = NetworkType.Ethereum;
    public string Erc721ContractAddress { get; } = "0x71902F99902339d7ce1F994C12155f4350BCD226";
    public string Erc1155ContractAddress { get; } = "0x80b45421881c0452A6e70148Fc928fA33107cEb3";
}
