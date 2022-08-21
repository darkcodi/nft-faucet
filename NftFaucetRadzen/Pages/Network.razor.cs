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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
                Name = "Custom",
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
                Id = Guid.NewGuid(),
                Name = "Polygon Mainnet",
                ChainId = 137,
                Currency = "MATIC",
                ImageName = "polygon.svg",
                IsSupported = false,
                IsTestnet = false,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Id = Guid.NewGuid(),
                Name = "Polygon Mumbai",
                ChainId = 80001,
                Currency = "MATIC",
                ImageName = "polygon-black.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = false,
            },
        };

        protected NetworkModel[] SolanaNetworks { get; } = new[]
        {
            new NetworkModel
            {
                Id = Guid.NewGuid(),
                Name = "Solana Mainnet",
                Currency = "SOL",
                ImageName = "solana.svg",
                IsSupported = false,
                IsTestnet = false,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Id = Guid.NewGuid(),
                Name = "Solana Devnet",
                Currency = "SOL",
                ImageName = "solana-black.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = false,
            },
            new NetworkModel
            {
                Id = Guid.NewGuid(),
                Name = "Solana Testnet",
                Currency = "SOL",
                ImageName = "solana-black.svg",
                IsSupported = true,
                IsTestnet = true,
                IsDeprecated = false,
            },
        };
    }
}
