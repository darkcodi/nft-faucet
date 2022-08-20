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
                Name = "Ethereum Mainnet",
                ChainId = 1,
                Currency = "ETH",
                IsSupported = false,
                IsTestnet = false,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Ropsten",
                ChainId = 3,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Ropsten",
                ChainId = 3,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Ropsten",
                ChainId = 3,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
            new NetworkModel
            {
                Name = "Rinkeby",
                ChainId = 4,
                Currency = "ETH",
                IsSupported = true,
                IsTestnet = true,
            },
        };
    }
}
