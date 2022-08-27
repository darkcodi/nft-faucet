using NftFaucetRadzen.Plugins.ProviderPlugins.GeneratedKeys.Providers;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.GeneratedKeys;

public class GeneratedKeysProviderPlugin : IProviderPlugin
{
    private IReadOnlyCollection<IProvider> Providers { get; } = new IProvider[]
    {
        new GeneratedKeysProvider(),
    };

    public IReadOnlyCollection<IProvider> GetProviders() => Providers;
}
