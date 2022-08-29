namespace NftFaucetRadzen.Plugins.UploadPlugins;

public interface IUploader
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsInitialized { get; }
}
