using NftFaucetRadzen.Plugins.UploadPlugins.NftStorage.Uploaders;

namespace NftFaucetRadzen.Plugins.UploadPlugins.NftStorage;

public class NftStorageUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new NftStorageUploader(),
    };
}
