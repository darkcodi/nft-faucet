namespace NftFaucetRadzen.Models;

public class ProviderModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string ImageName { get; set; }
    public bool IsSupported { get; set; }
}
