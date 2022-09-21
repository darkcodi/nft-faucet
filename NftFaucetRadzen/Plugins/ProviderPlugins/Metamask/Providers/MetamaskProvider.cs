using CSharpFunctionalExtensions;
using Ethereum.MetaMask.Blazor;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Function;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Services;
using NftFaucetRadzen.Utils;

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

    public async Task<string> GetAddress()
        => Address ?? await MetaMaskService.GetSelectedAccountAsync();

    public async Task<long> GetBalance()
        => (long) await MetaMaskService.GetBalanceAsync();

    public async Task<bool> EnsureNetworkMatches(INetwork network)
        => network.Type == NetworkType.Ethereum && network.ChainId != null && network.ChainId == await GetChainId();

    private async Task<ulong?> GetChainId()
    {
        var chainHex = await MetaMaskService.GetSelectedChainAsync();
        if (string.IsNullOrEmpty(chainHex) || chainHex == "0x")
        {
            return null;
        }

        var chainId = (ulong) Convert.ToInt64(chainHex, 16);
        return chainId;
    }

    public async Task<Result<string>> Mint(MintRequest mintRequest)
    {
        if (mintRequest.Network.Type != NetworkType.Ethereum)
        {
            throw new InvalidOperationException("Invalid network type for this provider");
        }

        switch (mintRequest.Contract.Type)
        {
            case ContractType.Erc721:
            {
                return await ResultWrapper.Wrap(async () =>
                {
                    var contractAddress = mintRequest.Contract.Address;
                    var transfer = new Erc721MintFunction
                    {
                        To = mintRequest.DestinationAddress,
                        Uri = mintRequest.UploadLocation.Location,
                    };
                    var data = transfer.Encode();
                    var transactionHash = await MetaMaskService.SendTransactionAsync(contractAddress, 0, data);
                    if (string.IsNullOrEmpty(transactionHash))
                    {
                        throw new Exception("Operation was cancelled or RPC node failure");
                    }

                    return transactionHash;
                });
            }
            case ContractType.Erc1155:
            {
                return await ResultWrapper.Wrap(async () =>
                {
                    var contractAddress = mintRequest.Contract.Address;
                    var transfer = new Erc1155MintFunction
                    {
                        To = mintRequest.DestinationAddress,
                        Amount = mintRequest.TokensAmount,
                        Uri = mintRequest.UploadLocation.Location,
                    };
                    var data = transfer.Encode();
                    var transactionHash = await MetaMaskService.SendTransactionAsync(contractAddress, 0, data);
                    if (string.IsNullOrEmpty(transactionHash))
                    {
                        throw new Exception("Operation was cancelled or RPC node failure");
                    }

                    return transactionHash;
                });
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
