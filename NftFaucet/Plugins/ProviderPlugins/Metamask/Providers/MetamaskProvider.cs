using CSharpFunctionalExtensions;
using Ethereum.MetaMask.Blazor;
using NftFaucet.Components.CardList;
using NftFaucet.Models;
using NftFaucet.Models.Function;
using NftFaucet.Plugins.NetworkPlugins;
using NftFaucet.Services;

namespace NftFaucet.Plugins.ProviderPlugins.Metamask.Providers;

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

    public async Task<string> GetAddress()
        => Address ?? await MetaMaskService.GetSelectedAccountAsync();

    public async Task<Balance> GetBalance(INetwork network)
    {
        if (!IsConfigured)
            return null;

        var balance = (long) await MetaMaskService.GetBalanceAsync();
        return new Balance
        {
            Amount = balance,
            Currency = "wei",
        };
    }

    public Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        var chainId = Convert.ToUInt64(ChainId, 16);
        var matchingNetwork = allKnownNetworks.FirstOrDefault(x => x.ChainId != null && x.ChainId.Value == chainId);
        return Task.FromResult(matchingNetwork);
    }

    public async Task<string> Mint(MintRequest mintRequest)
    {
        if (mintRequest.Network.Type != NetworkType.Ethereum)
        {
            throw new InvalidOperationException("Invalid network type for this provider");
        }

        Function transfer = mintRequest.Contract.Type switch
        {
            ContractType.Erc721 => new Erc721MintFunction
            {
                To = mintRequest.DestinationAddress,
                Uri = mintRequest.UploadLocation.Location,
            },
            ContractType.Erc1155 => new Erc1155MintFunction
            {
                To = mintRequest.DestinationAddress,
                Amount = mintRequest.TokensAmount,
                Uri = mintRequest.UploadLocation.Location,
            },
            _ => throw new ArgumentOutOfRangeException(),
        };

        var contractAddress = mintRequest.Contract.Address;
        var data = transfer.Encode();
        var transactionHash = await MetaMaskService.SendTransactionAsync(contractAddress, 0, data);
        if (string.IsNullOrEmpty(transactionHash))
        {
            throw new Exception("Operation was cancelled or RPC node failure");
        }

        return transactionHash;
    }

    public Task<string> GetState()
        => Task.FromResult(string.Empty);

    public Task SetState(string state)
        => Task.CompletedTask;
}