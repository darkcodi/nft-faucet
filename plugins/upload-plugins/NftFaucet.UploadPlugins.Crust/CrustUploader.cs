using System.Text;
using CSharpFunctionalExtensions;
using Nethereum.Signer;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.UploadPlugins.Crust.ApiClients;
using NftFaucet.UploadPlugins.Crust.Models;
using RestEase;

namespace NftFaucet.UploadPlugins.Crust;

public class CrustUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("5cc1a8bf-9b6a-4fa7-a262-1161bcca3cd5");
    public string Name { get; } = "Crust";
    public string ShortName { get; } = "Crust";
    public string ImageName { get; } = "crust.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured { get; } = true;

    private string AuthHeader { get; set; }

    public Property[] GetProperties()
        => new[] {new Property {Value = "Very slow, but zero-config"}};

    public ConfigurationItem[] GetConfigurationItems()
        => Array.Empty<ConfigurationItem>();

    public Task<Result> Configure(ConfigurationItem[] configurationItems)
        => Task.FromResult(Result.Success());

    public async Task<Uri> Upload(string fileName, string fileType, byte[] fileData)
    {
        var fileUploadRequest = ToMultipartContent(fileName, fileType, fileData);
        var uploadClient = RestClient.For<ICrustUploadApiClient>();
        var authHeader = GetAuthHeader();
        uploadClient.Auth = authHeader;

        using var uploadFileResponse = await uploadClient.UploadFile(fileUploadRequest);
        if (!uploadFileResponse.ResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Status: {(int) uploadFileResponse.ResponseMessage.StatusCode}. Reason: {uploadFileResponse.ResponseMessage.ReasonPhrase}");
        }

        var uploadResponse = uploadFileResponse.GetContent();
        if (string.IsNullOrEmpty(uploadResponse?.Hash))
        {
            throw new Exception($"Unexpected response: {uploadFileResponse.StringContent}");
        }

        var pinningClient = RestClient.For<ICrustPinApiClient>();
        pinningClient.Auth = authHeader;
        var pinRequest = new PinRequest
        {
            cid = uploadResponse.Hash,
            name = fileName,
        };
        using var pinningResponse = await pinningClient.PinFile(pinRequest);
        if (!pinningResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Status: {(int) pinningResponse.StatusCode}. Reason: {pinningResponse.ReasonPhrase}");
        }

        return new Uri("https://gw.crustapps.net/ipfs/" + uploadResponse.Hash);
    }

    public Task<string> GetState()
        => Task.FromResult(string.Empty);

    public Task SetState(string state)
        => Task.CompletedTask;

    private MultipartContent ToMultipartContent(string fileName, string fileType, byte[] fileData)
    {
        var content = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(fileData);
        imageContent.Headers.Add("Content-Type", fileType);
        content.Add(imageContent, "\"file\"", $"\"{fileName}\"");
        return content;
    }

    private string GetAuthHeader()
    {
        if (AuthHeader != null)
            return AuthHeader;

        var key = EthereumKey.GenerateNew();
        var address = key.Address.ToLowerInvariant();
        var signer = new EthereumMessageSigner();
        var user = $"eth-{address}";
        var password = signer.EncodeUTF8AndSign(address, new EthECKey(key.PrivateKey));
        var basicAuth = $"{user}:{password}";
        var base64BasicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(basicAuth));
        var authHeader = $"Basic {base64BasicAuth}";

        AuthHeader = authHeader;
        return authHeader;
    }
}
