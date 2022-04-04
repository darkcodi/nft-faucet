using NftFaucet.ApiClients.Models;
using RestEase;

namespace NftFaucet.ApiClients;

[BaseAddress("https://gw.crustapps.net")]
public interface ICrustApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("api/v0/add")]
    Task<UploadResponse> UploadFile([Body] MultipartContent content, [Query("pin")] bool pin = true);
}
