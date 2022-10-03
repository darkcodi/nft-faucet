using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins.Models;

public abstract class Uploader : DefaultEntity, IUploader
{
    public abstract Task<Uri> Upload(string fileName, string fileType, byte[] fileData);
}
