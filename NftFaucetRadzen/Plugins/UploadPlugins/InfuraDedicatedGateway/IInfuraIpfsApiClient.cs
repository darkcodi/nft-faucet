using System.Net.Http.Headers;
using RestEase;

namespace NftFaucetRadzen.Plugins.UploadPlugins.InfuraDedicatedGateway;

[BaseAddress("https://ipfs.infura.io:5001")]
public interface IInfuraIpfsApiClient
{
    [Header("Authorization")]
    public AuthenticationHeaderValue Auth { get; set; }

    [Post("api/v0/add")]
    [AllowAnyStatusCode]
    Task<Response<UploadResponse>> UploadFile([Body] MultipartContent content);

    [Post("api/v0/version")]
    [AllowAnyStatusCode]
    Task<Response<VersionResponse>> GetVersion();
}