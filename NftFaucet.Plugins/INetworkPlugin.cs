using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins;

public interface INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; }
}
