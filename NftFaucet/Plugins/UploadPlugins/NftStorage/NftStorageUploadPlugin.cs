using NftFaucet.Plugins.UploadPlugins.NftStorage.Uploaders;

namespace NftFaucet.Plugins.UploadPlugins.NftStorage;

public class NftStorageUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new NftStorageUploader(),
    };
}
