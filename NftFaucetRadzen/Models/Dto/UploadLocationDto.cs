namespace NftFaucetRadzen.Models.Dto;

public class UploadLocationDto
{
    public Guid Id { get; set; }
    public Guid TokenId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UploaderId { get; set; }
}
