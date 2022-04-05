using NftFaucet.ApiClients.Models;
using RestEase;

namespace NftFaucet.ApiClients;

[BaseAddress("https://ipfs.infura.io:5001")]
public interface IInfuraIpfsApiClient
{
    [Post("api/v0/add")]
    Task<UploadResponse> UploadFile([Body] MultipartContent content, [Query("stream-channels")] bool streamChannels = true);
}
