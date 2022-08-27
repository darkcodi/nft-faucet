using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Services.Abstractions;

public interface IPluginLoader
{
    public IReadOnlyCollection<INetworkPlugin> NetworkPlugins { get; }
    public IReadOnlyCollection<IProviderPlugin> ProviderPlugins { get; }

    public bool ArePluginsLoaded { get; }
    public void LoadPlugins();
    public void EnsurePluginsLoaded();
}
