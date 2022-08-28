using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class ContractsPage : BasicComponent
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
        Contracts = AppState.SelectedNetwork?.DeployedContracts?.ToArray() ?? Array.Empty<IContract>();
        RefreshData();
    }

    private IContract[] Contracts { get; set; }
    private CardListItem[] Data { get; set; }

    private void RefreshData()
    {
        Data = Contracts.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IContract contract)
        => new CardListItem
        {
            Id = contract.Id,
            Header = contract.Name,
            Properties = new[]
            {
                new CardListItemProperty
                {
                    Name = "Symbol",
                    Value = contract.Symbol,
                },
                new CardListItemProperty
                {
                    Name = "Address",
                    Value = contract.Address,
                },
                new CardListItemProperty
                {
                    Name = "TxHash",
                    Value = contract.DeploymentTxHash,
                },
                new CardListItemProperty
                {
                    Name = "DeployedAt",
                    Value = contract.DeployedAt.ToString(CultureInfo.InvariantCulture),
                },
            },
            Badges = new[]
            {
                contract.IsVerified
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Verified"}
                    : null,
                new CardListItemBadge {Style = BadgeStyle.Light, Text = contract.Type.ToString()},
            }.Where(x => x != null).ToArray(),
        };
}
