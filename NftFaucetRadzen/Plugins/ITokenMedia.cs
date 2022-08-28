namespace NftFaucetRadzen.Plugins;

public interface ITokenMedia
{
    public string FileData { get; }
    public string FileName { get; }
    public long FileSize { get; }
}
