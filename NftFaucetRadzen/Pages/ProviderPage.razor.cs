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
        // AppState.Storage.GeneratedKey = EthereumKey.GenerateNew();
        Providers = PluginLoader.ProviderPlugins.SelectMany(x => x.GetProviders()).Select(MapCardListItem).ToArray();
        EnhanceProviderModels(Providers);
        AddRecommendationsBadge(Providers);
    }

    private CardListItem[] Providers { get; set; }

    private void EnhanceProviderModels(CardListItem[] models)
    {
        var genKeyModel = models.FirstOrDefault(x => x.Id == Guid.Parse("ded55b2b-8139-4251-a0fc-ba620f9727c9"));
        if (genKeyModel != null)
        {
            genKeyModel.Properties = new[]
            {
                new CardListItemProperty { Name = "Private key", Value = AppState.Storage.GeneratedKey?.PrivateKey ?? "<null>" },
                new CardListItemProperty { Name = "Address", Value = AppState.Storage.GeneratedKey?.Address ?? "<null>" },
            };
        }

        var metamaskModel = models.FirstOrDefault(x => x.Id == Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987"));
        if (metamaskModel != null)
        {
            metamaskModel.Properties = new[]
            {
                new CardListItemProperty { Name = "Installed", Value = "yes" },
                new CardListItemProperty { Name = "Connected", Value = "no" },
            };
        }
    }

    private static CardListItem MapCardListItem(IProvider provider)
        => new CardListItem
        {
            Id = provider.Id,
            ImageName = provider.ImageName,
            Header = provider.Name,
            IsDisabled = !provider.IsSupported,
            Properties = Array.Empty<CardListItemProperty>(),
            Badges = new[]
            {
                !provider.IsSupported ? new CardListItemBadge { Style = BadgeStyle.Light, Text = "Not Supported" } : null,
            }.Where(x => x != null).ToArray(),
        };

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
