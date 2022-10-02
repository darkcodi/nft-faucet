namespace NftFaucet.Infrastructure.Models.Dto;

public class TokenDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string MainFileName { get; set; }
    public string MainFileType { get; set; }
    public string MainFileData { get; set; }
    public long? MainFileSize { get; set; }
    public string CoverFileName { get; set; }
    public string CoverFileType { get; set; }
    public string CoverFileData { get; set; }
    public long? CoverFileSize { get; set; }
}
