using System.Reflection;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;
using NftFaucetRadzen.Services.Abstractions;

namespace NftFaucetRadzen.Services;

public class PluginLoader : IPluginLoader
{
    public IReadOnlyCollection<INetworkPlugin> NetworkPlugins { get; private set; }
    public IReadOnlyCollection<IProviderPlugin> ProviderPlugins { get; private set; }

    public bool ArePluginsLoaded { get; private set; }

    public void LoadPlugins()
    {
        if (ArePluginsLoaded)
        {
            throw new InvalidOperationException("Plugins are already loaded");
        }

        var assembly = Assembly.GetExecutingAssembly();
        var allTypes = assembly.GetTypes();
        
        var networkPluginTypes = allTypes.Where(x => x.IsClass && typeof(INetworkPlugin).IsAssignableFrom(x)).ToArray();
        var providerPluginTypes = allTypes.Where(x => x.IsClass && typeof(IProviderPlugin).IsAssignableFrom(x)).ToArray();

        var networkPlugins = networkPluginTypes.Select(x => (INetworkPlugin) Activator.CreateInstance(x)).ToArray();
        var providerPlugins = providerPluginTypes.Select(x => (IProviderPlugin) Activator.CreateInstance(x)).ToArray();

        NetworkPlugins = networkPlugins;
        ProviderPlugins = providerPlugins;

        ArePluginsLoaded = true;
    }
    
    public void EnsurePluginsLoaded()
    {
        if (!ArePluginsLoaded)
        {
            LoadPlugins();
        }
    }
}
