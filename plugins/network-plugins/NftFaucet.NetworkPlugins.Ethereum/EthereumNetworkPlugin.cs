using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.NetworkPlugins.Ethereum.Networks;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Ethereum;

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
