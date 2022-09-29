using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.ProviderPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class ProvidersPage : BasicComponent
{
    protected override void OnInitialized()
    {
        Providers = AppState.PluginStorage.Providers.Where(x => AppState.SelectedNetwork != null && x.IsNetworkSupported(AppState.SelectedNetwork)).ToArray();
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
        base.OnInitialized();
    }

    private IProvider[] Providers { get; set; }
    private CardListItem[] ProviderCards { get; set; }

    private void RefreshCards()
    {
        ProviderCards = Providers.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IProvider provider)
    {
        var configuration = provider.GetConfiguration();
        return new CardListItem
        {
            Id = provider.Id,
            ImageLocation = provider.ImageName != null ? "./images/" + provider.ImageName : null,
            Header = provider.Name,
            IsDisabled = !provider.IsSupported,
            Properties = provider.GetProperties().ToArray(),
            SelectionIcon = provider.IsConfigured ? CardListItemSelectionIcon.Checkmark : CardListItemSelectionIcon.Warning,
            Badges = new[]
            {
                (Settings?.RecommendedProviders?.Contains(provider.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !provider.IsSupported
                    ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"}
                    : null,
            }.Where(x => x != null).ToArray(),
            Configuration = configuration == null ? null : new CardListItemConfiguration
            {
                Objects = configuration.Objects,
                ConfigureAction = async x =>
                {
                    var result = await configuration.ConfigureAction(x);
                    RefreshCards();
                    if (result.IsSuccess)
                    {
                        await StateRepository.SaveProviderState(provider);
                    }
                    return result;
                },
            },
        };
    }

    private async Task OnProviderChange()
    {
        await SaveAppState();
    }
}
