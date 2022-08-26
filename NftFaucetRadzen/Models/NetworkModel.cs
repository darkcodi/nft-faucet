using NftFaucetRadzen.Models.Enums;

namespace NftFaucetRadzen.Models;

public class NetworkModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public ulong? ChainId { get; set; }
    public string Currency { get; set; }
    public string ImageName { get; set; }
    public bool IsSupported { get; set; }
    public bool IsTestnet { get; set; }
    public bool IsDeprecated { get; set; }
    public NetworkType Type { get; set; }
    public string Erc721ContractAddress { get; set; }
    public string Erc1155ContractAddress { get; set; }
}
