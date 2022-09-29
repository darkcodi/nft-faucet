namespace NftFaucetRadzen.Models.Dto;

public class TokenDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageFileName { get; set; }
    public string ImageFileType { get; set; }
    public string ImageFileData { get; set; }
    public long? ImageFileSize { get; set; }
}
