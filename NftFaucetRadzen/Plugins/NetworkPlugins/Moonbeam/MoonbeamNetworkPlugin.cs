using NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam.Networks;

namespace NftFaucetRadzen.Plugins.NetworkPlugins.Moonbeam;

public class MoonbeamNetworkPlugin : INetworkPlugin
{
    private IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new MoonbeamNetwork(),
        new MoonriverNetwork(),
        new MoonbaseAlphaNetwork(),
    };

    public IReadOnlyCollection<INetwork> GetNetworks() => Networks;
}
