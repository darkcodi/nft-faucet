using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.UploadPlugins.Crust;

public class CrustUploaderPlugin : IUploaderPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new CrustUploader(),
    };
}
