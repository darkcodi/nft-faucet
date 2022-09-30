using System.Reflection;
using NftFaucet.Plugins.NetworkPlugins;
using NftFaucet.Plugins.ProviderPlugins;
using NftFaucet.Plugins.UploadPlugins;

namespace NftFaucet.Services;

public class PluginLoader
{
    public IReadOnlyCollection<INetworkPlugin> NetworkPlugins { get; private set; }
    public IReadOnlyCollection<IProviderPlugin> ProviderPlugins { get; private set; }
    public IReadOnlyCollection<IUploadPlugin> UploadPlugins { get; private set; }

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
        var uploadPluginTypes = allTypes.Where(x => x.IsClass && typeof(IUploadPlugin).IsAssignableFrom(x)).ToArray();

        NetworkPlugins = networkPluginTypes.Select(x => (INetworkPlugin) Activator.CreateInstance(x)).ToArray();
        ProviderPlugins = providerPluginTypes.Select(x => (IProviderPlugin) Activator.CreateInstance(x)).ToArray();
        UploadPlugins = uploadPluginTypes.Select(x => (IUploadPlugin) Activator.CreateInstance(x)).ToArray();

        ArePluginsLoaded = true;
    }
}