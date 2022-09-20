using CSharpFunctionalExtensions;
using Ethereum.MetaMask.Blazor;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Services;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Metamask.Providers;

public class MetamaskProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987");
    public string Name { get; } = "Metamask";
    public string ShortName { get; } = "Metamask";
    public string ImageName { get; } = "metamask_fox.svg";
    public bool IsSupported { get; } = true;
    public bool IsConfigured => IsMetamaskAvailable && IsSiteConnected && !string.IsNullOrEmpty(Address);

    private IMetaMaskService MetaMaskService { get; set; }
    private RefreshMediator RefreshMediator { get; set; }

    private bool IsMetamaskAvailable { get; set; }
    private bool IsSiteConnected { get; set; }
    private string Address { get; set; }
    private string ChainId { get; set; }

    public async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        MetaMaskService = serviceProvider.GetRequiredService<IMetaMaskService>();
        RefreshMediator = serviceProvider.GetRequiredService<RefreshMediator>();

        IsMetamaskAvailable = await MetaMaskService.IsMetaMaskAvailableAsync();
        if (IsMetamaskAvailable)
        {
            IsSiteConnected = await MetaMaskService.IsSiteConnectedAsync();
        }

        if (IsSiteConnected)
        {
            Address = await MetaMaskService.GetSelectedAccountAsync();
            ChainId = await MetaMaskService.GetSelectedChainAsync();
        }
    }

    public CardListItemProperty[] GetProperties()
    {
        var list = new List<CardListItemProperty>(3)
        {
            new CardListItemProperty
            {
                Name = "Installed", Value = IsMetamaskAvailable ? "YES" : "NO",
                ValueColor = IsMetamaskAvailable ? "green" : "red"
            },
            new CardListItemProperty
            {
                Name = "Connected", Value = IsSiteConnected ? "YES" : "NO",
                ValueColor = IsSiteConnected ? "green" : "red"
            },
        };
        if (!string.IsNullOrEmpty(Address))
        {
            list.Add(new CardListItemProperty
            {
                Name = "Address",
                Value = Address,
            });
        }
        
        return list.ToArray();
    }

    public CardListItemConfiguration GetConfiguration()
    {
        var addressInput = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Input,
            Name = "Address",
            Placeholder = "<null>",
            Value = Address,
            IsDisabled = true,
        };
        var chainInput = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Input,
            Name = "ChainId",
            Placeholder = "<null>",
            Value = ChainId,
            IsDisabled = true,
        };
        var connectButton = new CardListItemConfigurationObject
        {
            Type = CardListItemConfigurationObjectType.Button,
            Name = "Connect",
            ClickAction = async () =>
            {
                var address = await MetaMaskService.ConnectAsync();
                if (!string.IsNullOrEmpty(address))
                {
                    IsSiteConnected = true;
                    Address = address;
                    addressInput.Value = Address;
                    ChainId = await MetaMaskService.GetSelectedChainAsync();
                    chainInput.Value = ChainId;
                }
                else
                {
                    IsSiteConnected = false;
                    Address = null;
                    addressInput.Value = null;
                    ChainId = null;
                    chainInput.Value = null;
                }

                RefreshMediator.NotifyStateHasChangedSafe();
            },
        };
        return new CardListItemConfiguration
        {
            Objects = new[] { addressInput, chainInput, connectButton },
            ConfigureAction = objects =>
            {
                // IsConfigured = true;
                return Task.FromResult(Result.Success());
            },
        };
    }

    public bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;
}
