using NftFaucet.Models.Enums;

namespace NftFaucet.Services;

public interface IIpfsService
{
    Uri GetUrlToGateway(Uri ipfsUrl, IpfsGatewayType gateway);
    Task<Uri> Upload(string fileName, string fileType, string url);
    Task<Uri> Upload(string fileName, string fileType, byte[] fileBytes);
}
