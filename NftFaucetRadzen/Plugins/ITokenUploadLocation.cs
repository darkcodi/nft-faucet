namespace NftFaucetRadzen.Plugins;

public interface ITokenUploadLocation
{
    public Guid Id { get; }
    public Guid TokenId { get; }
    public string Name { get; }
    public string Location { get; }
    public DateTime CreatedAt { get; }
    public Guid UploaderId { get; }
}
