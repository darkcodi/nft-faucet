namespace NftFaucet.Models;

public class NewFileModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string MainFileData { get; set; }
    public string MainFileName { get; set; }
    public long? MainFileSize { get; set; }
    public string CoverFileData { get; set; }
    public string CoverFileName { get; set; }
    public long? CoverFileSize { get; set; }
}
