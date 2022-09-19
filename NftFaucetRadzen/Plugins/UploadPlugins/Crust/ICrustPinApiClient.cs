using RestEase;

namespace NftFaucetRadzen.Plugins.UploadPlugins.Crust;

[BaseAddress("https://pin.crustcode.com")]
public interface ICrustPinApiClient
{
    [Header("Authorization")]
    public string Auth { get; set; }

    [Post("psa/pins")]
    Task PinFile([Body] PinRequest request);
}
