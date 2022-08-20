using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using Radzen;

namespace NftFaucetRadzen.Pages
{
    public partial class Network
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected NetworkModel[] EthereumNetworks { get; } = new[]
        {
            new NetworkModel
            {
                Name = "Mainnet",
                ChainId = 1,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = false,
                IsTestnet = false,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Name = "Ropsten",
                ChainId = 3,
                Currency = "ETH",
                ImageName = "ethereum-gray.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = true,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                ImageName = "ethereum-gray.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = true,
            },
            new NetworkModel
            {
                Name = "Goerli",
                ChainId = 5,
                Currency = "ETH",
                ImageName = "ethereum-gray.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Name = "Kovan",
                ChainId = 42,
                Currency = "ETH",
                ImageName = "ethereum-gray.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = true,
            },
            new NetworkModel
            {
                Name = "Kiln",
                ChainId = 1337802,
                Currency = "ETH",
                ImageName = "ethereum-gray.svg",
                IsSupported = false,
                IsTestnet = true,
                IsDeprecated = true,
            },
            new NetworkModel
            {
                Name = "Sepolia",
                ChainId = 11155111,
                Currency = "SEP",
                ImageName = "ethereum-gray.svg",
                IsSupported = false,
                IsTestnet = true,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Name = "Custom",
                ChainId = 1337,
                Currency = "???",
                ImageName = "ethereum-gray.svg",
                IsSupported = false,
                IsTestnet = true,
                IsDeprecated = false,
            },
        };

        protected NetworkModel[] PolygonNetworks { get; } = new[]
        {
            new NetworkModel
            {
                Name = "Polygon Mainnet",
                ChainId = 137,
                Currency = "MATIC",
                ImageName = "polygon-matic.svg",
                IsSupported = false,
                IsTestnet = false,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Name = "Polygon Mumbai",
                ChainId = 80001,
                Currency = "MATIC",
                ImageName = "polygon-matic.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = false,
            },
        };
    }
}
