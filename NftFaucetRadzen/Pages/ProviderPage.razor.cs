using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
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
        Providers = PluginLoader.ProviderPlugins.SelectMany(x => x.GetProviders()).Select(MapCardListItem).ToArray();
        AddRecommendationsBadge(Providers);
    }

    private CardListItem[] Providers { get; set; }

    private static CardListItem MapCardListItem(IProvider provider)
    {
        var cardItem = new CardListItem
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
                !provider.IsSupported ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"} : null,
            }.Where(x => x != null).ToArray(),
        };
        Action initAction = () =>
        {
            provider.Initialize();
            cardItem.Properties = provider.GetProperties().Select(x => new CardListItemProperty
            {
                Name = x.Name,
                Value = x.Value,
            }).ToArray();
        };
        cardItem.Buttons = new[]
        {
            !provider.IsInitialized ? new CardListItemButton { Name = "Initialize", Action = initAction, Style = ButtonStyle.Secondary } : null,
        }.Where(x => x != null).ToArray();
        return cardItem;
    }

    private void AddRecommendationsBadge(CardListItem[] providers)
    {
        foreach (var provider in providers)
        {
            if (Settings.RecommendedProviders.Contains(provider.Id))
            {
                var newBadge = new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"};
                provider.Badges = new[] {newBadge}.Concat(provider.Badges).ToArray();
            }
        }
    }
}
