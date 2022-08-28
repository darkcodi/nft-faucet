namespace NftFaucetRadzen.Plugins;

public interface ITokenUploadLocation
{
    public Guid Id { get; }
    public string Name { get; }
    public StorageType StorageType { get; }
    public UploadProvider UploadProvider { get; }
}
