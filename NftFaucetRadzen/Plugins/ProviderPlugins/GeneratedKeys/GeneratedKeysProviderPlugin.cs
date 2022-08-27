using NftFaucetRadzen.Plugins.ProviderPlugins.GeneratedKeys.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.GeneratedKeys;

public class GeneratedKeysProviderPlugin : IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new GeneratedKeysProvider(),
    };
}
