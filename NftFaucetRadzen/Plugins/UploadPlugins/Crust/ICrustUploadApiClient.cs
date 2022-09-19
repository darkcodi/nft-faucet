using NftFaucetRadzen.Plugins.UploadPlugins.Infura;
using RestEase;

namespace NftFaucetRadzen.Plugins.UploadPlugins.Crust;

[BaseAddress("https://gw.crustapps.net")]
public interface ICrustUploadApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("api/v0/add")]
    Task<UploadResponse> UploadFile([Body] MultipartContent content, [Query("pin")] bool pin = true);
}
