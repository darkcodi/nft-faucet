namespace NftFaucet.UploadPlugins.NftStorage.Models;

public class UploadResponse
{
    public bool Ok { get; set; }
    public UploadResponseValue Value { get; set; }

    public class UploadResponseValue
    {
        public string Cid { get; set; }
    }
}
