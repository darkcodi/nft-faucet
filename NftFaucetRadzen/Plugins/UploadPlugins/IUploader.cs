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
    public IReadOnlyCollection<ConfigurationItem> GetConfigurationItems();
    public Task<Result> TryInitialize(IReadOnlyCollection<ConfigurationItem> configurationItems);
    public Task<Result<Uri>> Upload(IToken token);
}
