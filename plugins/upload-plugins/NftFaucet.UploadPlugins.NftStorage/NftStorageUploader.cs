using CSharpFunctionalExtensions;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.UploadPlugins.NftStorage;

public class NftStorageUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("ece2123a-cca7-4266-91e7-bc73680cf218");
    public string Name { get; } = "nft.storage";
    public string ShortName { get; } = "NftStorage";
    public string ImageName { get; } = "nft-storage.svg";
    public bool IsSupported { get; } = false;
    public bool IsConfigured { get; } = false;

    public Property[] GetProperties()
        => Array.Empty<Property>();

    public ConfigurationItem[] GetConfigurationItems()
        => Array.Empty<ConfigurationItem>();

    public Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        throw new NotImplementedException();
    }

    public Task<Uri> Upload(string fileName, string fileType, byte[] fileData)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetState()
        => Task.FromResult(string.Empty);

    public Task SetState(string state)
        => Task.CompletedTask;
}
