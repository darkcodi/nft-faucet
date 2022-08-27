using System.Reflection;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Services;

public class PluginLoader
{
    public IReadOnlyCollection<INetworkPlugin> NetworkPlugins { get; private set; }
    public IReadOnlyCollection<IProviderPlugin> ProviderPlugins { get; private set; }

    public bool ArePluginsLoaded { get; private set; }

    public void EnsurePluginsLoaded()
    {
        if (ArePluginsLoaded)
        {
            return;
        }

        var assembly = Assembly.GetExecutingAssembly();
        var allTypes = assembly.GetTypes();
        
        var networkPluginTypes = allTypes.Where(x => x.IsClass && typeof(INetworkPlugin).IsAssignableFrom(x)).ToArray();
        var providerPluginTypes = allTypes.Where(x => x.IsClass && typeof(IProviderPlugin).IsAssignableFrom(x)).ToArray();

        NetworkPlugins = networkPluginTypes.Select(x => (INetworkPlugin) Activator.CreateInstance(x)).ToArray();
        ProviderPlugins = providerPluginTypes.Select(x => (IProviderPlugin) Activator.CreateInstance(x)).ToArray();

        ArePluginsLoaded = true;
    }
}
