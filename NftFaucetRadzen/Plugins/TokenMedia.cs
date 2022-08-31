namespace NftFaucetRadzen.Plugins;

public class TokenMedia : ITokenMedia
{
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string FileData { get; set; }
    public long FileSize { get; set; }
}
