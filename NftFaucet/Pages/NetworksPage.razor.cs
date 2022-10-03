using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models.Abstraction;
using Radzen;

namespace NftFaucet.Pages;

public partial class NetworksPage : BasicComponent
{
    protected override void OnInitialized()
    {
        Networks = AppState.PluginStorage.Networks
            .GroupBy(x => x.SubType)
            .ToDictionary(x => x.Key, x => x.OrderBy(v => v.Order ?? int.MaxValue).Select(MapCardListItem).ToArray());
    }

    private Dictionary<NetworkSubtype, CardListItem[]> Networks { get; set; }

    private CardListItem MapCardListItem(INetwork model)
        => new CardListItem
        {
            Id = model.Id,
            ImageLocation = model.ImageName != null ? "./images/" + model.ImageName : null,
            Header = model.Name,
            IsDisabled = !model.IsSupported,
            Properties = new[]
            {
                model.ChainId != null ? new CardListItemProperty { Name = "ChainID", Value = model.ChainId?.ToString() } : null,
                !string.IsNullOrEmpty(model.Currency) ? new CardListItemProperty { Name = "Currency", Value = model.Currency } : null,
            }.Where(x => x != null).ToArray(),
            Badges = new[]
            {
                (Settings?.RecommendedNetworks?.Contains(model.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !model.IsSupported ? new CardListItemBadge { Style = BadgeStyle.Light, Text = "Not Supported" } : null,
                !model.IsTestnet ? new CardListItemBadge { Style = BadgeStyle.Danger, Text = "Mainnet" } : null,
                model.IsDeprecated ? new CardListItemBadge { Style = BadgeStyle.Warning, Text = "Deprecated" } : null,
            }.Where(x => x != null).ToArray(),
        };

    private async Task OnNetworkChange()
    {
        var currentNetwork = AppState.SelectedNetwork;
        AppState.UserStorage.SelectedContracts = Array.Empty<Guid>();

        var currentProvider = AppState.SelectedProvider;
        if (currentNetwork == null || (currentProvider != null && !currentProvider.IsNetworkSupported(currentNetwork)))
        {
            AppState.UserStorage.SelectedProviders = Array.Empty<Guid>();
        }

        await SaveAppState();
    }
}
