using Ethereum.MetaMask.Blazor;
using Microsoft.Extensions.DependencyInjection;
using NftFaucet.Domain.Function;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.Plugins.Models.Enums;

namespace NftFaucet.WalletPlugins.Metamask;

public class MetamaskWallet : Wallet
{
    public override Guid Id { get; } = Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987");
    public override string Name { get; } = "Metamask";
    public override string ShortName { get; } = "Metamask";
    public override string ImageName { get; } = "metamask_fox.svg";
    public override bool IsInitialized { get; protected set; }
    public override bool IsConfigured => IsMetamaskAvailable && IsSiteConnected && !string.IsNullOrEmpty(Address);

    private IMetaMaskService MetaMaskService { get; set; }
    private bool IsMetamaskAvailable { get; set; }
    private bool IsSiteConnected { get; set; }
    private string Address { get; set; }
    private string ChainId { get; set; }

    public override async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        MetaMaskService = serviceProvider.GetRequiredService<IMetaMaskService>();

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

        IsInitialized = true;
    }

    public override Property[] GetProperties()
    {
        var list = new List<Property>(3)
        {
            new Property
            {
                Name = "Installed", Value = IsMetamaskAvailable ? "YES" : "NO",
                ValueColor = IsMetamaskAvailable ? "green" : "red"
            },
            new Property
            {
                Name = "Connected", Value = IsSiteConnected ? "YES" : "NO",
                ValueColor = IsSiteConnected ? "green" : "red"
            },
        };
        if (!string.IsNullOrEmpty(Address))
        {
            list.Add(new Property
            {
                Name = "Address",
                Value = Address,
            });
        }
        
        return list.ToArray();
    }

    public override ConfigurationItem[] GetConfigurationItems()
    {
        var addressInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "Address",
            Placeholder = "<null>",
            Value = Address,
            IsDisabled = true,
        };
        var chainInput = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Input,
            Name = "ChainId",
            Placeholder = "<null>",
            Value = ChainId,
            IsDisabled = true,
        };
        var connectButton = new ConfigurationItem
        {
            DisplayType = UiDisplayType.Button,
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
            },
        };
        return new[] { addressInput, chainInput, connectButton };
    }

    public override bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Ethereum;

    public override async Task<string> GetAddress()
        => Address ?? await MetaMaskService.GetSelectedAccountAsync();

    public override async Task<Balance> GetBalance(INetwork network)
    {
        if (!IsConfigured)
            return null;

        var balance = await MetaMaskService.GetBalanceAsync();
        return new Balance(balance, "wei");
    }

    public override Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        var chainId = Convert.ToUInt64(ChainId, 16);
        var matchingNetwork = allKnownNetworks.FirstOrDefault(x => x.ChainId != null && x.ChainId.Value == chainId);
        return Task.FromResult(matchingNetwork);
    }

    public override async Task<string> Mint(MintRequest mintRequest)
    {
        if (mintRequest.Network.Type != NetworkType.Ethereum)
        {
            throw new InvalidOperationException("Invalid network type for this wallet");
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
}
