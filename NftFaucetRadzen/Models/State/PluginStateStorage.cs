using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;
using NftFaucetRadzen.Plugins.UploadPlugins;

namespace NftFaucetRadzen.Models.State;

public class PluginStateStorage
{
    public ICollection<INetwork> Networks { get; set; }
    public ICollection<IProvider> Providers { get; set; }
    public ICollection<IUploader> Uploaders { get; set; }
    public ICollection<IContract> Contracts { get; set; }
}
