namespace NftFaucetRadzen.Plugins;

public class TokenUploadLocation : ITokenUploadLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UploaderId { get; set; }
}
