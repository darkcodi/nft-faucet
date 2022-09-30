namespace NftFaucet.Plugins.UploadPlugins;

public interface IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; }
}
