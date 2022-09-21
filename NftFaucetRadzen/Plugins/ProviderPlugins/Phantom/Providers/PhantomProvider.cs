using CSharpFunctionalExtensions;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Phantom.Providers;

public class PhantomProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("ae860901-5441-463c-a16e-4786f041500d");
    public string Name { get; } = "Phantom";
    public string ShortName { get; } = "Phantom";
    public string ImageName { get; } = "phantom.svg";
    public bool IsSupported { get; } = false;
    public bool IsConfigured { get; private set; }

    public Task InitializeAsync(IServiceProvider serviceProvider)
        => Task.CompletedTask;

    public CardListItemProperty[] GetProperties()
        => new CardListItemProperty[]
        {
            new CardListItemProperty{ Name = "Installed", Value = "YES" },
            new CardListItemProperty{ Name = "Connected", Value = IsConfigured ? "YES" : "NO" },
        };

    public CardListItemConfiguration GetConfiguration()
        => new CardListItemConfiguration
        {
            Objects = new[]
            {
                new CardListItemConfigurationObject
                {
                    Type = CardListItemConfigurationObjectType.Button,
                    Name = "Connect",
                    ClickAction = () => { },
                }
            },
            ConfigureAction = objects =>
            {
                IsConfigured = true;
                return Task.FromResult(Result.Success());
            },
        };

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;

    public Task<string> GetAddress()
        => throw new NotImplementedException();

    public Task<long> GetBalance()
    {
        throw new NotImplementedException();
    }

    public Task<bool> EnsureNetworkMatches(INetwork network)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> Mint(MintRequest mintRequest)
    {
        throw new NotImplementedException();
    }
}
