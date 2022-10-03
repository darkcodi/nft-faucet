using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins;

public interface IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; }
}
