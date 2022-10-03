using System.Net.Http.Headers;
using CSharpFunctionalExtensions;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Enums;
using NftFaucet.UploadPlugins.NftStorage.ApiClients;
using RestEase;

namespace NftFaucet.UploadPlugins.NftStorage;

public class NftStorageUploader : Uploader
{
    public override Guid Id { get; } = Guid.Parse("ece2123a-cca7-4266-91e7-bc73680cf218");
    public override string Name { get; } = "nft.storage";
    public override string ShortName { get; } = "NftStorage";
    public override string ImageName { get; } = "nft-storage.svg";
    public override bool IsConfigured { get; protected set; }

    public override int? Order { get; } = 1;

    private string ApiKey { get; set; }

    public override Property[] GetProperties()
        => new Property[]
        {
            IsConfigured
                ? new Property {Name = "Configured", Value = "YES", ValueColor = "green"}
                : new Property {Name = "Configured", Value = "NO", ValueColor = "red"}
        };

    public override ConfigurationItem[] GetConfigurationItems()
    {
        var instructionsText = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Text,
            Name = "Instructions",
            Value = "Go to 'https://nft.storage', register for a free tier, create API key and copy it",
        };
        var apiKeyInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "API Key",
            Placeholder = "<jwt token>",
            Value = ApiKey,
        };
        return new[] { instructionsText, apiKeyInput };
    }

    public override async Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        var apiKey = configurationItems[1].Value;

        if (string.IsNullOrEmpty(apiKey))
        {
            return Result.Failure("API key is null or empty");
        }

        var apiClient = GetApiClient(apiKey);
        using var testResponse = await apiClient.ListFiles();
        if (!testResponse.ResponseMessage.IsSuccessStatusCode)
        {
            return Result.Failure<Uri>($"Status: {(int) testResponse.ResponseMessage.StatusCode}. Reason: {testResponse.ResponseMessage.ReasonPhrase}");
        }

        ApiKey = apiKey;
        IsConfigured = true;
        return Result.Success();
    }

    public override async Task<Uri> Upload(string fileName, string fileType, byte[] fileData)
    {
        var apiClient = GetApiClient(ApiKey);
        var fileUploadRequest = ToMultipartContent(fileName, fileType, fileData);
        using var response = await apiClient.UploadFile(fileUploadRequest);
        if (!response.ResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status: {(int) response.ResponseMessage.StatusCode}. Reason: {response.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = response.GetContent();
        if (uploadResponse == null || !uploadResponse.Ok || string.IsNullOrEmpty(uploadResponse.Value?.Cid))
        {
            throw new Exception($"Unexpected response: {response.StringContent}");
        }

        return new Uri("https://nftstorage.link/ipfs/" + uploadResponse.Value.Cid + "/" + fileName);
    }

    public override Task<string> GetState()
        => Task.FromResult(ApiKey);

    public override Task SetState(string state)
    {
        if (string.IsNullOrEmpty(state))
            return Task.CompletedTask;

        ApiKey = state;
        IsConfigured = true;
        return Task.CompletedTask;
    }

    private static INftStorageApiClient GetApiClient(string apiKey)
    {
        var uploadClient = RestClient.For<INftStorageApiClient>();
        uploadClient.Auth = new AuthenticationHeaderValue("Bearer", apiKey);
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
