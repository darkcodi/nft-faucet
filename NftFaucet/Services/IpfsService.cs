using System.Text;
using NftFaucet.ApiClients;
using NftFaucet.ApiClients.Models;
using NftFaucet.Models;
using NftFaucet.Models.Enums;
using NftFaucet.Options;
using RestEase;

namespace NftFaucet.Services;

public class IpfsService : IIpfsService
{
    private const string IpfsUrlPrefix = "ipfs://";
    private readonly IpfsBlockchainContext _blockchainContext;
    private readonly Settings _settings;

    public IpfsService(IpfsBlockchainContext blockchainContext, Settings settings)
    {
        _blockchainContext = blockchainContext;
        _settings = settings;
    }

    public Uri GetUrlToGateway(Uri ipfsUrl, IpfsGatewayType gateway)
    {
        if (ipfsUrl == null)
            throw new ArgumentNullException(nameof(ipfsUrl));

        if (!ipfsUrl.OriginalString.StartsWith(IpfsUrlPrefix, StringComparison.InvariantCultureIgnoreCase))
            throw new ArgumentException(nameof(ipfsUrl));

        if (gateway == IpfsGatewayType.None)
            return ipfsUrl;

        var options = _settings.GetIpfsGatewayOptions(gateway);
        var prefix = options.BaseUrl;
        if (!prefix.EndsWith("/"))
            prefix += "/";

        return new Uri(prefix + ipfsUrl.OriginalString.Replace(IpfsUrlPrefix, string.Empty));
    }

    public async Task<Uri> Upload(string fileName, string fileType, string url)
    {
        using var httpClient = new HttpClient();
        var fileBytes = await httpClient.GetByteArrayAsync(new Uri(url));
        return await Upload(fileName, fileType, fileBytes);
    }

    public async Task<Uri> Upload(string fileName, string fileType, byte[] fileBytes)
    {
        var fileUploadRequest = ToMultipartContent(fileName, fileType, fileBytes);
        var uploadClient = RestClient.For<IInfuraIpfsApiClient>();
        var response = await uploadClient.UploadFile(fileUploadRequest);

        var pinningClient = RestClient.For<ICrustApiClient>();
        pinningClient.Auth = GenerateAuthHeader();
        var pinRequest = new PinRequest
        {
            cid = response.Hash,
            name = fileName,
        };
        await pinningClient.PinFile(pinRequest);

        var uri = IpfsUrlPrefix + response.Hash;
        return new Uri(uri);
    }

    private MultipartContent ToMultipartContent(string fileName, string fileType, byte[] bytes)
    {
        var content = new MultipartFormDataContent();

        var imageContent = new ByteArrayContent(bytes);
        imageContent.Headers.Add("Content-Type", fileType);
        content.Add(imageContent, "\"file\"", $"\"{fileName}\"");

        return content;
    }

    private string GenerateAuthHeader()
    {
        if (!_blockchainContext.IsInitialized)
            throw new InvalidOperationException("Blockchain context is not filled");

        var user = $"eth-{_blockchainContext.Address.ToLower()}";
        var password = _blockchainContext.SignedMessage;
        var basicAuth = $"{user}:{password}";
        var base64BasicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(basicAuth));
        var authHeader = $"Basic {base64BasicAuth}";

        return authHeader;
    }
}
