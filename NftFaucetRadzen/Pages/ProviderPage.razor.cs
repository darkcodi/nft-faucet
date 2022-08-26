using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
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

    protected CardListItem[] Providers { get; private set; } = new CardListItem[]
    {
        new CardListItem
        {
            ImageName = "metamask_fox.svg",
            Header = "Metamask",
            IsDisabled = false,
            Properties = new[]
            {
                new CardListItemProperty { Name = "Installed", Value = "yes" },
                new CardListItemProperty { Name = "Connected", Value = "no" },
            },
            Badges = new[]
            {
                new CardListItemBadge { Style = BadgeStyle.Success, Text = "Recommended" },
            },
        }
    };
}
