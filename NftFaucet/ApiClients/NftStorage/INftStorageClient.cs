using NftFaucet.ApiClients.NftStorage.Models;
using RestEase;

namespace NftFaucet.ApiClients.NftStorage;

[BaseAddress("https://api.nft.storage")]
[Header("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweGQxQTdlMDk3QjdEOTNGZURkNTU1RTE1M2FGMzg4OTkwQzBGQ0EwY2MiLCJpc3MiOiJuZnQtc3RvcmFnZSIsImlhdCI6MTY0ODgxNTYxODkxOSwibmFtZSI6IlRlc3QifQ.CqCC_jo8TIsA1JMGC_NsthRAQKSCJbKjSp5irZwr54g")]
public interface INftStorageClient
{
    [Post("upload")]
    Task<UploadResponse> UploadFile([Body] MultipartContent content);
}
