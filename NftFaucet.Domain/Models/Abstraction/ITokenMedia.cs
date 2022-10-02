namespace NftFaucet.Domain.Models.Abstraction;

public interface ITokenMedia
{
    public string FileName { get; }
    public string FileType { get; }
    public string FileData { get; }
    public long FileSize { get; }
}
