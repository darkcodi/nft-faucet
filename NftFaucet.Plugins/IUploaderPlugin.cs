using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins;

public interface IUploaderPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; }
}
