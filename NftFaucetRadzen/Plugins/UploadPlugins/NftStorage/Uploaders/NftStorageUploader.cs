using CSharpFunctionalExtensions;

namespace NftFaucetRadzen.Plugins.UploadPlugins.NftStorage.Uploaders;

public class NftStorageUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("ece2123a-cca7-4266-91e7-bc73680cf218");
    public string Name { get; } = "nft.storage";
    public string ShortName { get; } = "NftStorage";
    public string ImageName { get; } = "nft-storage.svg";
    public bool IsSupported { get; } = false;
    public bool IsInitialized { get; } = false;

    public IReadOnlyCollection<ConfigurationItem> GetConfigurationItems()
    {
        throw new NotImplementedException();
    }

    public Task<Result> TryInitialize(IReadOnlyCollection<ConfigurationItem> configurationItems)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Uri>> Upload(IToken token)
    {
        throw new NotImplementedException();
    }
}
