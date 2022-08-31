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
    
    private string ProjectId { get; set; }
    private string ProjectSecret { get; set; }

    public IReadOnlyCollection<ConfigurationItem> GetConfigurationItems()
        => new[]
        {
            new ConfigurationItem
            {
                Name = "Project ID",
                Placeholder = "<ProjectId>",
            },
            new ConfigurationItem
            {
                Name = "API Key Secret",
                Placeholder = "<ProjectSecret>",
            },
        };

    public async Task<Result> TryInitialize(IReadOnlyCollection<ConfigurationItem> configurationItems)
    {
        if (configurationItems == null || configurationItems.Count != 2)
        {
            return Result.Failure("Invalid configuration items count");
        }

        var projectId = configurationItems.First().Value;
        if (string.IsNullOrEmpty(projectId))
        {
            return Result.Failure("ProjectId is null or empty");
        }

        var projectSecret = configurationItems.Skip(1).First().Value;
        if (string.IsNullOrEmpty(projectSecret))
        {
            return Result.Failure("ProjectSecret is null or empty");
        }

        var apiClient = GetInfuraClient(projectId, projectSecret);
        var response = await apiClient.GetVersion();
        if (!response.ResponseMessage.IsSuccessStatusCode)
        {
            return Result.Failure<Uri>($"Status: {(int) response.ResponseMessage.StatusCode}. Reason: {response.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = response.GetContent();
        if (string.IsNullOrEmpty(uploadResponse?.Version))
        {
            return Result.Failure<Uri>($"Unexpected response: {response.StringContent}");
        }

        ProjectId = projectId;
        ProjectSecret = projectSecret;
        IsInitialized = true;
        return Result.Success();
    }

    public async Task<Result<Uri>> Upload(IToken token)
    {
        var apiClient = GetInfuraClient(ProjectId, ProjectSecret);
        var fileUploadRequest = ToMultipartContent(token.Image.FileName, token.Image.FileType, token.Image.FileData);
        var response = await apiClient.UploadFile(fileUploadRequest);
        if (!response.ResponseMessage.IsSuccessStatusCode)
        {
            return Result.Failure<Uri>($"Status: {(int) response.ResponseMessage.StatusCode}. Reason: {response.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = response.GetContent();
        if (string.IsNullOrEmpty(uploadResponse?.Hash))
        {
            return Result.Failure<Uri>($"Unexpected response: {response.StringContent}");
        }

        var uri = new Uri("ipfs://" + uploadResponse.Hash);
        return uri;
    }

    private IInfuraIpfsApiClient GetInfuraClient(string projectId, string projectSecret)
    {
        var uploadClient = RestClient.For<IInfuraIpfsApiClient>();
        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{projectId}:{projectSecret}"));
        uploadClient.Auth = new AuthenticationHeaderValue("Basic", auth);
        return uploadClient;
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
