using CSharpFunctionalExtensions;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;

namespace NftFaucet.Plugins.Models.Abstraction;

public interface IProvider
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsInitialized { get; }
    public bool IsSupported { get; }
    public bool IsConfigured { get; }

    public Task InitializeAsync(IServiceProvider serviceProvider);
    public Property[] GetProperties();
    public ConfigurationItem[] GetConfigurationItems();
    public Task<Result> Configure(ConfigurationItem[] configurationItems);
    public bool IsNetworkSupported(INetwork network);
    public Task<string> GetAddress();
    public Task<Balance> GetBalance(INetwork network);
    public Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork);
    public Task<string> Mint(MintRequest mintRequest);
    public Task<string> GetState();
    public Task SetState(string state);
}
