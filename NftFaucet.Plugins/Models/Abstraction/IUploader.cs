using CSharpFunctionalExtensions;

namespace NftFaucet.Plugins.Models.Abstraction;

public interface IUploader
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsConfigured { get; }

    public Property[] GetProperties();
    public ConfigurationItem[] GetConfigurationItems();
    public Task<Result> Configure(ConfigurationItem[] configurationItems);
    public Task<Uri> Upload(string fileName, string fileType, byte[] fileData);
    public Task<string> GetState();
    public Task SetState(string state);
}
