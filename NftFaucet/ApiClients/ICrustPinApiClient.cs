using NftFaucet.ApiClients.Models;
using RestEase;

namespace NftFaucet.ApiClients;

[BaseAddress("https://pin.crustcode.com")]
public interface ICrustPinApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("psa/pins")]
    Task PinFile([Body] PinRequest request);
}
