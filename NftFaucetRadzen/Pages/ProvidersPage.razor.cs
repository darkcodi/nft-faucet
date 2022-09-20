using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.ProviderPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class ProvidersPage : BasicComponent
{
    [Inject]
    public IServiceProvider ServiceProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Providers = AppState.Storage.Providers.Where(x => AppState.SelectedNetwork != null && x.IsNetworkSupported(AppState.SelectedNetwork)).ToArray();
        foreach (var provider in Providers)
        {
            await provider.InitializeAsync(ServiceProvider);
        }
        
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
                ConfigureAction = x =>
                {
                    var result = configuration.ConfigureAction(x);
                    RefreshCards();
                    return result;
                },
            },
        };
    }
}
