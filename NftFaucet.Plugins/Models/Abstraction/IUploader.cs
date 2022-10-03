namespace NftFaucet.Plugins.Models.Abstraction;

public interface IUploader : INamedEntity, IStateful, IInitializable, IEntityWithProperties, IConfigurable
{
    public Task<Uri> Upload(string fileName, string fileType, byte[] fileData);
}
