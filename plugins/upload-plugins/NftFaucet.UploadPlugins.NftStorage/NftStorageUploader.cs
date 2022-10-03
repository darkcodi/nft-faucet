using NftFaucet.Plugins.Models;

namespace NftFaucet.UploadPlugins.NftStorage;

public class NftStorageUploader : Uploader
{
    public override Guid Id { get; } = Guid.Parse("ece2123a-cca7-4266-91e7-bc73680cf218");
    public override string Name { get; } = "nft.storage";
    public override string ShortName { get; } = "NftStorage";
    public override string ImageName { get; } = "nft-storage.svg";
    public override bool IsSupported { get; } = false;

    public override int? Order { get; } = 1;

    public override Task<Uri> Upload(string fileName, string fileType, byte[] fileData)
    {
        throw new NotImplementedException();
    }
}
