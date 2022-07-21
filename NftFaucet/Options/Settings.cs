using NftFaucet.Models.Enums;

namespace NftFaucet.Options;

public class Settings
{
    public NetworkOptions[] Networks { get; set; }
    public IpfsGatewayOptions[] IpfsGateways { get; set; }

    public NetworkOptions GetEthereumNetworkOptions(NetworkChain network)
        => Networks.FirstOrDefault(x => x.Id == network);

    public IpfsGatewayOptions GetIpfsGatewayOptions(IpfsGatewayType gateway)
        => IpfsGateways.FirstOrDefault(x => x.Id == gateway);
}
