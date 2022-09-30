using NftFaucet.Plugins.UploadPlugins.Infura.Uploaders;

namespace NftFaucet.Plugins.UploadPlugins.Infura;

public class InfuraUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new InfuraUploader(),
    };
}
