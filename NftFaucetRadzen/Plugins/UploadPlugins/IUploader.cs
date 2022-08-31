using CSharpFunctionalExtensions;

namespace NftFaucetRadzen.Plugins.UploadPlugins;

public interface IUploader
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsInitialized { get; }
    public IReadOnlyCollection<ConfigurationItem> GetConfigurationItems();
    public Task<Result> TryInitialize(IReadOnlyCollection<ConfigurationItem> configurationItems);
    public Task<Result<Uri>> Upload(IToken token);
}
