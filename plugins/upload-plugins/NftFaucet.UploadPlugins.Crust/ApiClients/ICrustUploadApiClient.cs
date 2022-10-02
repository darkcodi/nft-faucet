using NftFaucet.UploadPlugins.Crust.Models;
using RestEase;

namespace NftFaucet.UploadPlugins.Crust.ApiClients;

[BaseAddress("https://gw.crustapps.net")]
public interface ICrustUploadApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("api/v0/add")]
    [AllowAnyStatusCode]
    Task<Response<UploadResponse>> UploadFile([Body] MultipartContent content, [Query("pin")] bool pin = true);
}
