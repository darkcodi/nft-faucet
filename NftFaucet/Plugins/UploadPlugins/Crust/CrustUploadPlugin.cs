using NftFaucet.Plugins.UploadPlugins.Crust.Uploaders;

namespace NftFaucet.Plugins.UploadPlugins.Crust;

public class CrustUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new CrustUploader(),
    };
}
