using System.Net.Http.Headers;
using NftFaucet.UploadPlugins.Infura.Models;
using RestEase;

namespace NftFaucet.UploadPlugins.Infura.ApiClients;

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
