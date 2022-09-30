using NftFaucet.Plugins.NetworkPlugins;
using NftFaucet.Plugins.ProviderPlugins;
using NftFaucet.Plugins.UploadPlugins;

namespace NftFaucet.Models.State;

public class PluginStateStorage
{
    public ICollection<INetwork> Networks { get; set; }
    public ICollection<IProvider> Providers { get; set; }
    public ICollection<IUploader> Uploaders { get; set; }
    public ICollection<IContract> Contracts { get; set; }
}
