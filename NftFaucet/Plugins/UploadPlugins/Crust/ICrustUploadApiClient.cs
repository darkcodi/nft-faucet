using NftFaucet.Plugins.UploadPlugins.Infura;
using RestEase;

namespace NftFaucet.Plugins.UploadPlugins.Crust;

[BaseAddress("https://gw.crustapps.net")]
public interface ICrustUploadApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("api/v0/add")]
    [AllowAnyStatusCode]
    Task<Response<UploadResponse>> UploadFile([Body] MultipartContent content, [Query("pin")] bool pin = true);
}
