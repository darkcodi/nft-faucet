using System.Net.Http.Headers;
using NftFaucet.UploadPlugins.NftStorage.Models;
using RestEase;

namespace NftFaucet.UploadPlugins.NftStorage.ApiClients;

[BaseAddress("https://api.nft.storage")]
public interface INftStorageApiClient
{
    [Header("Authorization")]
    public AuthenticationHeaderValue Auth { get; set; }

    [Post("upload")]
    [AllowAnyStatusCode]
    Task<Response<UploadResponse>> UploadFile([Body] MultipartContent content);

    [Get]
    [AllowAnyStatusCode]
    Task<Response<object>> ListFiles([Query("limit")] int limit = 1);
}
