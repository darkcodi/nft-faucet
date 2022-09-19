using System.Text;
using CSharpFunctionalExtensions;
using Nethereum.Signer;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using RestEase;

namespace NftFaucetRadzen.Plugins.UploadPlugins.Crust.Uploaders;

public class CrustUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("5cc1a8bf-9b6a-4fa7-a262-1161bcca3cd5");
    public string Name { get; } = "Crust";
    public string ShortName { get; } = "Crust";
    public string ImageName { get; } = "crust.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured { get; } = true;

    private string AuthHeader { get; set; }

    public CardListItemProperty[] GetProperties()
        => new[] {new CardListItemProperty {Value = "Very slow, but zero-config"}};

    public CardListItemConfiguration GetConfiguration()
        => null;

    public async Task<Result<Uri>> Upload(IToken token)
    {
        var fileUploadRequest = ToMultipartContent(token.Image.FileName, token.Image.FileType, token.Image.FileData);
        var uploadClient = RestClient.For<ICrustUploadApiClient>();
        var authHeader = GetAuthHeader();
        uploadClient.Auth = authHeader;
        var response = await uploadClient.UploadFile(fileUploadRequest);

        var pinningClient = RestClient.For<ICrustPinApiClient>();
        pinningClient.Auth = authHeader;
        var pinRequest = new PinRequest
        {
            cid = response.Hash,
            name = token.Image.FileName,
        };
        await pinningClient.PinFile(pinRequest);

        return new Uri("https://gw.crustapps.net/ipfs/" + response.Hash);
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
