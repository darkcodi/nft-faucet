using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.NetworkPlugins.Moonbeam.Networks;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.NetworkPlugins.Moonbeam;

public class MoonbeamNetworkPlugin : INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; } = new INetwork[]
    {
        new MoonbeamNetwork(),
        new MoonriverNetwork(),
        new MoonbaseAlphaNetwork(),
    };
}
