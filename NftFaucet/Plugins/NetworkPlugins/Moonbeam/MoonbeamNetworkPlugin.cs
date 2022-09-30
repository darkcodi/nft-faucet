using NftFaucet.Plugins.NetworkPlugins.Moonbeam.Networks;

namespace NftFaucet.Plugins.NetworkPlugins.Moonbeam;

public class MoonbeamNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new MoonbeamNetwork(),
        new MoonriverNetwork(),
        new MoonbaseAlphaNetwork(),
    };
}
