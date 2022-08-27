using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins.ProviderPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class ProviderPage : BasicComponent
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
        Providers = AppState.Storage.Providers.Where(x => x.IsNetworkSupported(AppState.SelectedNetwork)).ToArray();
        RefreshData();
    }

    private IProvider[] Providers { get; set; }
    private CardListItem[] Data { get; set; }

    private void RefreshData()
    {
        Data = Providers.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IProvider provider)
        => new CardListItem
        {
            Id = provider.Id,
            ImageName = provider.ImageName,
            Header = provider.Name,
            IsDisabled = !provider.IsSupported,
            Properties = provider.GetProperties().Select(x => new CardListItemProperty
            {
                Name = x.Name,
                Value = x.Value,
            }).ToArray(),
            Badges = new[]
            {
                (Settings?.RecommendedProviders?.Contains(provider.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !provider.IsSupported
                    ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"}
                    : null,
            }.Where(x => x != null).ToArray(),
            Buttons = new[]
            {
                !provider.IsInitialized
                    ? new CardListItemButton { Name = "Initialize", Style = ButtonStyle.Secondary, Action = () =>
                    {
                        provider.Initialize();
                        RefreshData();
                    }}
                    : null,
            }.Where(x => x != null).ToArray()
        };
}
