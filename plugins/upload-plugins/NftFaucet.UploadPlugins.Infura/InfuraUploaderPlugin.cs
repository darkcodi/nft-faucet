using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.UploadPlugins.Infura;

public class InfuraUploaderPlugin : IUploaderPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new InfuraUploader(),
    };
}
