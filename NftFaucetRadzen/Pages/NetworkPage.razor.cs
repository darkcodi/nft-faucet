using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Enums;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class NetworkPage : BasicComponent
{
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    protected override void OnInitialized()
    {
        EthereumNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Ethereum).Select(MapCardListItem).ToArray();
        PolygonNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Polygon).Select(MapCardListItem).ToArray();
        BscNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Bsc).Select(MapCardListItem).ToArray();
        OptimismNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Optimism).Select(MapCardListItem).ToArray();
        MoonbaseNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Moonbase).Select(MapCardListItem).ToArray();
        ArbitrumNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Arbitrum).Select(MapCardListItem).ToArray();
        AvalancheNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Avalanche).Select(MapCardListItem).ToArray();
        SolanaNetworks = Settings.Networks.Where(x => x.Type == NetworkType.Solana).Select(MapCardListItem).ToArray();
    }

    protected CardListItem[] EthereumNetworks { get; private set; }
    protected CardListItem[] PolygonNetworks { get; private set; }
    protected CardListItem[] BscNetworks { get; private set; }
    protected CardListItem[] OptimismNetworks { get; private set; }
    protected CardListItem[] MoonbaseNetworks { get; private set; }
    protected CardListItem[] ArbitrumNetworks { get; private set; }
    protected CardListItem[] AvalancheNetworks { get; private set; }
    protected CardListItem[] SolanaNetworks { get; private set; }

    private static CardListItem MapCardListItem(NetworkModel model)
        => new CardListItem
        {
            Id = model.Id,
            ImageName = model.ImageName,
            Header = model.Name,
            IsDisabled = !model.IsSupported,
            Properties = new[]
            {
                new CardListItemProperty { Name = "ChainID", Value = model.ChainId?.ToString() },
                new CardListItemProperty { Name = "Currency", Value = model.Currency },
            },
            Badges = new[]
            {
                !model.IsSupported ? new CardListItemBadge { Style = BadgeStyle.Light, Text = "Not Supported" } : null,
                !model.IsTestnet ? new CardListItemBadge { Style = BadgeStyle.Danger, Text = "Mainnet" } : null,
                model.IsDeprecated ? new CardListItemBadge { Style = BadgeStyle.Warning, Text = "Deprecated" } : null,
            }.Where(x => x != null).ToArray(),
        };
}
