using NftFaucet.Models.Enums;

namespace NftFaucet.Options;

public class NetworkOptions
{
    public NetworkChain Id { get; set; }
    public string Erc721ContractAddress { get; set; }
    public string Erc1155ContractAddress { get; set; }
}
