using System.Net.Http.Headers;
using System.Text;
using CSharpFunctionalExtensions;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.Plugins.Models.Enums;
using NftFaucet.UploadPlugins.Infura.ApiClients;
using RestEase;

namespace NftFaucet.UploadPlugins.Infura;

public class InfuraUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("c0d79c82-8e35-4cd6-ad35-bbe378088308");
    public string Name { get; } = "Infura";
    public string ShortName { get; } = "Infura";
    public string ImageName { get; } = "infura_black.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured { get; private set; } = false;

    private const string DefaultGatewayUrl = "https://ipfs.infura.io:5001";
    private string ProjectId { get; set; }
    private string ProjectSecret { get; set; }
    private Uri DedicatedGatewayUrl { get; set; }

    public Property[] GetProperties()
    {
        var properties = new List<Property>();
        if (IsConfigured)
        {
            if (!string.IsNullOrEmpty(ProjectId))
            {
                properties.Add(new Property {Name = "ProjectId", Value = ProjectId});
            }

            if (DedicatedGatewayUrl != null)
            {
                properties.Add(new Property {Name = "DedicatedGatewayUrl", Value = DedicatedGatewayUrl.ToString()});
            }
        }
        else
        {
            properties.Add(new Property {Name = "Configured", Value = "NO", ValueColor = "red"});
        }

        return properties.ToArray();
    }

    public ConfigurationItem[] GetConfigurationItems()
    {
        var projectIdInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Project ID",
            Placeholder = "<ProjectId>",
            Value = ProjectId,
        };
        var projectSecretInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "API Key Secret",
            Placeholder = "<ProjectSecret>",
            Value = ProjectSecret,
        };
        var gatewayUrlInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Dedicated gateway URL (OPTIONAL)",
            Placeholder = "https://<your-subdomain>.infura-ipfs.io",
            Value = DedicatedGatewayUrl?.OriginalString,
        };
        return new[] { projectIdInput, projectSecretInput, gatewayUrlInput };
    }

    public async Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        var projectId = configurationItems[0].Value;
        var projectSecret = configurationItems[1].Value;
        var dedicatedGatewayUrl = configurationItems[2].Value;

        if (string.IsNullOrEmpty(projectId))
        {
            return Result.Failure("ProjectId is null or empty");
        }

        if (string.IsNullOrEmpty(projectSecret))
        {
            return Result.Failure("ProjectSecret is null or empty");
        }

        var apiClient = GetInfuraClient(projectId, projectSecret);
        using var versionResponse = await apiClient.GetVersion();
        if (!versionResponse.ResponseMessage.IsSuccessStatusCode)
        {
            return Result.Failure<Uri>($"Status: {(int) versionResponse.ResponseMessage.StatusCode}. Reason: {versionResponse.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = versionResponse.GetContent();
        if (string.IsNullOrEmpty(uploadResponse?.Version))
        {
            return Result.Failure<Uri>($"Unexpected response: {versionResponse.StringContent}");
        }

        if (!string.IsNullOrEmpty(dedicatedGatewayUrl))
        {
            if (!Uri.TryCreate(dedicatedGatewayUrl, UriKind.Absolute, out var parsedUrl))
            {
                return Result.Failure("Dedicated gateway URL is invalid");
            }

            if (parsedUrl.Scheme != "https")
            {
                return Result.Failure("Invalid url scheme for Dedicated gateway URL. Expected 'https'");
            }

            if (!parsedUrl.Host.EndsWith("infura-ipfs.io"))
            {
                return Result.Failure("Invalid url host for Dedicated gateway URL. Expected host ending with '.infura-ipfs.io'");
            }

            if (parsedUrl.PathAndQuery != "/")
            {
                return Result.Failure("Invalid url. Url should NOT contain path or query part.");
            }

            DedicatedGatewayUrl = parsedUrl;
        }
        else
        {
            DedicatedGatewayUrl = null;
        }

        ProjectId = projectId;
        ProjectSecret = projectSecret;
        IsConfigured = true;
        return Result.Success();
    }

    public async Task<Uri> Upload(string fileName, string fileType, byte[] fileData)
    {
        var apiClient = GetInfuraClient(ProjectId, ProjectSecret);
        var fileUploadRequest = ToMultipartContent(fileName, fileType, fileData);
        using var response = await apiClient.UploadFile(fileUploadRequest);
        if (!response.ResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status: {(int) response.ResponseMessage.StatusCode}. Reason: {response.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = response.GetContent();
        if (string.IsNullOrEmpty(uploadResponse?.Hash))
        {
            throw new Exception($"Unexpected response: {response.StringContent}");
        }

        if (DedicatedGatewayUrl != null)
        {
            return new Uri(DedicatedGatewayUrl, "ipfs/" + uploadResponse.Hash);
        }

        return new Uri("ipfs://" + uploadResponse.Hash);
    }

    public Task<string> GetState()
    {
        var parts = new string[]
        {
            ProjectId,
            ProjectSecret,
            DedicatedGatewayUrl?.OriginalString,
        };
        var state = string.Join("<|>", parts);
        return Task.FromResult(state);
    }

    public Task SetState(string state)
    {
        if (string.IsNullOrEmpty(state))
            return Task.CompletedTask;

        var parts = state.Split("<|>");
        ProjectId = parts[0];
        ProjectSecret = parts[1];
        var gatewayUrl = parts[2];
        DedicatedGatewayUrl = string.IsNullOrEmpty(gatewayUrl) ? null : new Uri(gatewayUrl);
        IsConfigured = true;
        return Task.CompletedTask;
    }

    private static IInfuraIpfsApiClient GetInfuraClient(string projectId, string projectSecret, string gatewayUrl = DefaultGatewayUrl)
    {
        var uploadClient = RestClient.For<IInfuraIpfsApiClient>(gatewayUrl);
        var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{projectId}:{projectSecret}"));
        uploadClient.Auth = new AuthenticationHeaderValue("Basic", auth);
        return uploadClient;
    }

    private MultipartContent ToMultipartContent(string fileName, string fileType, byte[] fileData)
    {
        var content = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(fileData);
        imageContent.Headers.Add("Content-Type", fileType);
        content.Add(imageContent, "\"file\"", $"\"{fileName}\"");
        return content;
    }
}
