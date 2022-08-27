using NftFaucetRadzen.Plugins.NetworkPlugins.Solana.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Solana;

public class SolanaNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new SolanaMainnetNetwork(),
        new SolanaDevnetNetwork(),
        new SolanaTestnetNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
