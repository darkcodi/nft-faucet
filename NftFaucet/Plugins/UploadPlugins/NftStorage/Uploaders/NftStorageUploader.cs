using CSharpFunctionalExtensions;
using NftFaucet.Components.CardList;

namespace NftFaucet.Plugins.UploadPlugins.NftStorage.Uploaders;

public class NftStorageUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("ece2123a-cca7-4266-91e7-bc73680cf218");
    public string Name { get; } = "nft.storage";
    public string ShortName { get; } = "NftStorage";
    public string ImageName { get; } = "nft-storage.svg";
    public bool IsSupported { get; } = false;
    public bool IsConfigured { get; } = false;

    public CardListItemProperty[] GetProperties()
        => Array.Empty<CardListItemProperty>();

    public CardListItemConfiguration GetConfiguration()
        => null;

    public Task<Result<Uri>> Upload(string fileName, string fileType, byte[] fileData)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetState()
        => Task.FromResult(string.Empty);

    public Task SetState(string state)
        => Task.CompletedTask;
}
