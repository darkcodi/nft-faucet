namespace NftFaucet.Plugins.Models.Abstraction;

public interface IInitializable
{
    public bool IsInitialized { get; }
    public Task InitializeAsync(IServiceProvider serviceProvider);
}
