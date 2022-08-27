using NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Ethereum;

public class EthereumNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new EthereumMainnetNetwork(),
        new RopstenNetwork(),
        new RinkebyNetwork(),
        new GoerliNetwork(),
        new KovanNetwork(),
        new KilnNetwork(),
        new SepoliaNetwork(),
        new CustomNetwork(),
    };
}
