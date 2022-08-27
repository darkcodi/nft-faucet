using Cryptography.ECDSA;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;
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

    protected CardListItem[] Providers { get; private set; }

    protected override void OnInitialized()
    {
        // AppState.Storage.GeneratedKey = EthereumKey.GenerateNew();
        Providers = new[]
        {
            new CardListItem
            {
                Id = Guid.Parse("ded55b2b-8139-4251-a0fc-ba620f9727c9"),
                ImageName = "ecdsa.svg",
                Header = "Generated keys",
                IsDisabled = false,
                Properties = new[]
                {
                    new CardListItemProperty { Name = "Private key", Value = AppState.Storage.GeneratedKey?.PrivateKey ?? "<null>" },
                    new CardListItemProperty { Name = "Address", Value = AppState.Storage.GeneratedKey?.Address ?? "<null>" },
                },
                Badges = Array.Empty<CardListItemBadge>(),
            },
            new CardListItem
            {
                Id = Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987"),
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
}
