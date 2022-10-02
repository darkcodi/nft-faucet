using NftFaucet.UploadPlugins.Crust.Models;
using RestEase;

namespace NftFaucet.UploadPlugins.Crust.ApiClients;

[BaseAddress("https://pin.crustcode.com")]
public interface ICrustPinApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("psa/pins")]
    [AllowAnyStatusCode]
    Task<HttpResponseMessage> PinFile([Body] PinRequest request);
}
