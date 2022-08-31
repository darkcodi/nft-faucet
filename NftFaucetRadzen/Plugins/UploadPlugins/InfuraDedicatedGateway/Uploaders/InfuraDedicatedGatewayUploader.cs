using System.Net.Http.Headers;
using System.Text;
using CSharpFunctionalExtensions;
using RestEase;

namespace NftFaucetRadzen.Plugins.UploadPlugins.InfuraDedicatedGateway.Uploaders;

public class InfuraDedicatedGatewayUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("c0d79c82-8e35-4cd6-ad35-bbe378088308");
    public string Name { get; } = "Infura dedicated IPFS gateway";
    public string ShortName { get; } = "InfuraDED";
    public string ImageName { get; } = "infura_black.svg";
    public bool IsSupported { get; } = true;
    public bool IsInitialized { get; private set; } = false;
    
    private Uri DedicatedGatewayUrl { get; set; }

    public IReadOnlyCollection<ConfigurationItem> GetConfigurationItems()
        => new[]
        {
            new ConfigurationItem
            {
                Name = "Dedicated gateway URL",
                Tooltip = "Specify full URL with 'https://' prefix and '.infura-ipfs.io' postfix",
                Value = "https://<your-subdomain>.infura-ipfs.io",
            },
        };

    public async Task<Result> TryInitialize(IReadOnlyCollection<ConfigurationItem> configurationItems)
    {
        if (configurationItems == null || configurationItems.Count != 1)
        {
            return Result.Failure("Invalid configuration items count");
        }

        var urlString = configurationItems.First().Value;
        if (string.IsNullOrEmpty(urlString))
        {
            return Result.Failure("Url string is null or empty");
        }

        if (!Uri.TryCreate(urlString, UriKind.Absolute, out var url))
        {
            return Result.Failure("Url string is invalid");
        }

        if (url.Scheme != "https")
        {
            return Result.Failure("Invalid url scheme. Expected 'https'");
        }

        if (!url.Host.EndsWith("infura-ipfs.io"))
        {
            return Result.Failure("Invalid url host. Expected host ending with '.infura-ipfs.io'");
        }

        DedicatedGatewayUrl = url;
        IsInitialized = true;
        return Result.Success();
    }

    public async Task<Result<Uri>> Upload(IToken token)
    {
        var uploadClient = RestClient.For<IInfuraIpfsApiClient>();
        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("<project-id>:<project-secret>"));
        uploadClient.Auth = new AuthenticationHeaderValue("Basic", auth);
        var fileUploadRequest = ToMultipartContent(token.Image.FileName, token.Image.FileType, token.Image.FileData);
        var response = await uploadClient.UploadFile(fileUploadRequest);
        var uri = new Uri("ipfs://" + response.Hash);
        return uri;
    }

    private MultipartContent ToMultipartContent(string fileName, string fileType, string fileData)
    {
        var content = new MultipartFormDataContent();

        var bytes = Base64DataToBytes(fileData);
        var imageContent = new ByteArrayContent(bytes);
        imageContent.Headers.Add("Content-Type", fileType);
        content.Add(imageContent, "\"file\"", $"\"{fileName}\"");

        return content;
    }

    private byte[] Base64DataToBytes(string fileData)
    {
        var index = fileData.IndexOf(';');
        var encoded = fileData.Substring(index + 8);
        return Convert.FromBase64String(encoded);
    }
}
