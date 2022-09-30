namespace NftFaucet.Plugins;

public interface ITokenMedia
{
    public string FileName { get; }
    public string FileType { get; }
    public string FileData { get; }
    public long FileSize { get; }
}
