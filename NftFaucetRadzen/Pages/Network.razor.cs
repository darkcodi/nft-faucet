using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Enums;
using NftFaucetRadzen.Options;
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

        [Inject]
        protected Settings Settings { get; set; }

        protected override void OnInitialized()
        {
            EthereumNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Ethereum).ToArray();
            PolygonNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Polygon).ToArray();
            BscNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Bsc).ToArray();
            OptimismNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Optimism).ToArray();
            MoonbaseNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Moonbase).ToArray();
            ArbitrumNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Arbitrum).ToArray();
            AvalancheNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Avalanche).ToArray();
            SolanaNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Solana).ToArray();
        }

        protected NetworkModel[] EthereumNetworks { get; private set; }
        protected NetworkModel[] PolygonNetworks { get; private set; }
        protected NetworkModel[] BscNetworks { get; private set; }
        protected NetworkModel[] OptimismNetworks { get; private set; }
        protected NetworkModel[] MoonbaseNetworks { get; private set; }
        protected NetworkModel[] ArbitrumNetworks { get; private set; }
        protected NetworkModel[] AvalancheNetworks { get; private set; }
        protected NetworkModel[] SolanaNetworks { get; private set; }

        public Guid[] SelectedNetworkIds { get; set; }
    }
}
