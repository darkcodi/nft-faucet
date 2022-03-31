namespace NftFaucet.ApiClients.NftStorage.Models;

public class UploadResponse
{
    public bool Ok { get; set; }
    public NftStorageResponseValue Value { get; set; }

    public class NftStorageResponseValue
    {
        public string Cid { get; set; }
        public string Type { get; set; }
        public NftStorageFile[] Files { get; set; }
        public long Size { get; set; }
    }

    public class NftStorageFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
