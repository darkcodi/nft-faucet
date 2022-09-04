using NftFaucetRadzen.Plugins.UploadPlugins.Infura.Uploaders;

namespace NftFaucetRadzen.Plugins.UploadPlugins.Infura;

public class InfuraUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new InfuraUploader(),
    };
}
