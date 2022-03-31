using NftFaucet.Models.Enums;

namespace NftFaucet.Options;

public class Settings
{
    public EthereumNetworkOptions[] EthereumNetworks { get; set; }
    public IpfsGatewayOptions[] IpfsGateways { get; set; }

    public EthereumNetworkOptions GetEthereumNetworkOptions(EthereumNetwork network)
        => EthereumNetworks.FirstOrDefault(x => x.Id == network);

    public IpfsGatewayOptions GetIpfsGatewayOptions(IpfsGatewayType gateway)
        => IpfsGateways.FirstOrDefault(x => x.Id == gateway);
}
