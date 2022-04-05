using NftFaucet.Models.Enums;

namespace NftFaucet.Models;

public class StateStorage
{
    public string TokenName { get; set; }
    public string TokenDescription { get; set; }
    public IpfsGatewayType IpfsGatewayType { get; set; } = IpfsGatewayType.Infura;
    public TokenType TokenType { get; set; } = TokenType.ERC721;
    public double TokenAmount { get; set; } = 1;
    public Uri LocalImageUrl { get; set; }
    public bool CanPreviewTokenFile { get; set; }
    public bool UploadIsInProgress { get; set; }
    public Uri IpfsImageUrl { get; set; }
    public string TokenMetadata { get; set; }
    public string TokenUrl { get; set; }
    public string DestinationAddress { get; set; }
}
