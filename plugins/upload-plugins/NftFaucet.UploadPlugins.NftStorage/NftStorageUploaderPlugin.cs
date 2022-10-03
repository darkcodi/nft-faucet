using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.UploadPlugins.NftStorage;

public class NftStorageUploaderPlugin : IUploaderPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new NftStorageUploader(),
    };
}
