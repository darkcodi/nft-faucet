using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class NetworksPage : BasicComponent
{
    protected override void OnInitialized()
    {
        Networks = AppState.Storage.Networks
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
                new CardListItemProperty { Name = "ChainID", Value = model.ChainId?.ToString() },
                new CardListItemProperty { Name = "Currency", Value = model.Currency },
            },
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

    private void OnNetworkChange()
    {
        var currentNetwork = AppState.SelectedNetwork;
        AppState.Storage.SelectedContracts = Array.Empty<Guid>();

        var currentProvider = AppState.SelectedProvider;
        if (currentNetwork == null || (currentProvider != null && !currentProvider.IsNetworkSupported(currentNetwork)))
        {
            AppState.Storage.SelectedProviders = Array.Empty<Guid>();
        }
    }
}
