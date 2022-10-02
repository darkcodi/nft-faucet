using CSharpFunctionalExtensions;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.ProviderPlugins.Phantom;

public class PhantomProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("ae860901-5441-463c-a16e-4786f041500d");
    public string Name { get; } = "Phantom";
    public string ShortName { get; } = "Phantom";
    public string ImageName { get; } = "phantom.svg";
    public bool IsInitialized { get; } = true;
    public bool IsSupported { get; } = false;
    public bool IsConfigured { get; private set; }

    public Task InitializeAsync(IServiceProvider serviceProvider)
        => Task.CompletedTask;

    public Property[] GetProperties()
        => new Property[]
        {
            new Property{ Name = "Installed", Value = "YES" },
            new Property{ Name = "Connected", Value = IsConfigured ? "YES" : "NO" },
        };

    public ConfigurationItem[] GetConfigurationItems()
        => Array.Empty<ConfigurationItem>();

    public Task<Result> Configure(ConfigurationItem[] configurationItems)
    {
        throw new NotImplementedException();
    }

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;

    public Task<string> GetAddress()
    {
        throw new NotImplementedException();
    }

    public Task<Balance> GetBalance(INetwork network)
    {
        throw new NotImplementedException();
    }

    public Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        throw new NotImplementedException();
    }

    public Task<string> Mint(MintRequest mintRequest)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetState()
        => Task.FromResult(string.Empty);

    public Task SetState(string state)
        => Task.CompletedTask;
}
