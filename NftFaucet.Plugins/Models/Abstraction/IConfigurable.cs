using CSharpFunctionalExtensions;

namespace NftFaucet.Plugins.Models.Abstraction;

public interface IConfigurable
{
    public bool IsConfigured { get; }
    public ConfigurationItem[] GetConfigurationItems();
    public Task<Result> Configure(ConfigurationItem[] configurationItems);
}
