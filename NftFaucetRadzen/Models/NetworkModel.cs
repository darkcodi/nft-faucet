namespace NftFaucetRadzen.Models;

public class NetworkModel
{
    public string Name { get; set; }
    public ulong? ChainId { get; set; }
    public string Currency { get; set; }
    public string ImageName { get; set; }
    public bool IsSupported { get; set; }
    public bool IsTestnet { get; set; }
    public bool IsDeprecated { get; set; }
}
