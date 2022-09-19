using NftFaucetRadzen.Plugins.UploadPlugins.Crust.Uploaders;

namespace NftFaucetRadzen.Plugins.UploadPlugins.Crust;

public class CrustUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new CrustUploader(),
    };
}
