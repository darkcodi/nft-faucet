using CSharpFunctionalExtensions;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins.Models;

public abstract class DefaultEntity : INamedEntity, IEntityWithOrder, IStateful, IInitializable, IEntityWithProperties, IConfigurable
{
    public abstract Guid Id { get; }
    public abstract string Name { get; }
    public abstract string ShortName { get; }
    public abstract string ImageName { get; }
    public virtual bool IsSupported { get; } = true;
    public virtual bool IsDeprecated { get; } = false;

    public virtual int? Order { get; } = null;

    public virtual Task<string> GetState() => Task.FromResult(string.Empty);
    public virtual Task SetState(string state) => Task.CompletedTask;

    public virtual bool IsInitialized { get; protected set; } = true;
    public virtual Task InitializeAsync(IServiceProvider serviceProvider) => Task.CompletedTask;

    public virtual Property[] GetProperties() => Array.Empty<Property>();

    public virtual bool IsConfigured { get; protected set; } = true;
    public virtual ConfigurationItem[] GetConfigurationItems() => Array.Empty<ConfigurationItem>();
    public virtual Task<Result> Configure(ConfigurationItem[] configurationItems) => Task.FromResult(Result.Success());
}
