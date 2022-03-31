using Newtonsoft.Json;
using NftFaucet.ApiClients.NftStorage;
using NftFaucet.Models.Enums;
using NftFaucet.Options;
using Serilog;

namespace NftFaucet.Services;

public class IpfsService : IIpfsService
{
    private const string IpfsUrlPrefix = "ipfs://";
    private readonly INftStorageClient _nftStorage;
    private readonly Settings _settings;

    public IpfsService(INftStorageClient nftStorage, Settings settings)
    {
        _nftStorage = nftStorage;
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
        var response = await _nftStorage.UploadFile(fileUploadRequest);
        Log.Information(JsonConvert.SerializeObject(response));
        var uri = IpfsUrlPrefix + response.Value.Cid + "/" + response.Value.Files.First().Name;
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
}
