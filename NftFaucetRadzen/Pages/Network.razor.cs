using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NftFaucetRadzen.Models;
using Radzen;
using Radzen.Blazor;

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

        protected NetworkModel[] Networks { get; } = new[]
        {
            new NetworkModel
            {
                Name = "Mainnet",
                ChainId = 1,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = false,
                IsTestnet = false,
            },
            new NetworkModel
            {
                Name = "Ropsten",
                ChainId = 3,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Goerli",
                ChainId = 5,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Kovan",
                ChainId = 42,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Kiln",
                ChainId = 1337802,
                Currency = "ETH",
                ImageName = "ethereum.svg",
                IsSupported = false,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Sepolia",
                ChainId = 11155111,
                Currency = "SEP",
                ImageName = "ethereum.svg",
                IsSupported = false,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Custom",
                ChainId = 1337,
                Currency = "???",
                ImageName = "ethereum.svg",
                IsSupported = false,
                IsTestnet = true,
            },
        };
    }
}
