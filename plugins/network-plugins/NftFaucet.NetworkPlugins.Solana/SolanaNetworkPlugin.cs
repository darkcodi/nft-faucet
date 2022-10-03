using NftFaucet.NetworkPlugins.Solana.Networks;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Solana;

public class SolanaNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new SolanaMainnetNetwork(),
        new SolanaDevnetNetwork(),
        new SolanaTestnetNetwork(),
    };
}
