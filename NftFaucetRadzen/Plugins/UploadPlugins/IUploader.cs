using CSharpFunctionalExtensions;
using NftFaucetRadzen.Components.CardList;

namespace NftFaucetRadzen.Plugins.UploadPlugins;

public interface IUploader
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsConfigured { get; }

    public CardListItemProperty[] GetProperties();
    public CardListItemConfiguration GetConfiguration();
    public Task<Result<Uri>> Upload(string fileName, string fileType, byte[] fileData);
    public Task<string> GetState();
    public Task SetState(string state);
}
