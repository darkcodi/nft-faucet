using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class TokensPage : BasicComponent
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
        // ToDo: Add loading from IndexedDB
        Tokens = Array.Empty<IToken>();
        RefreshData();
    }

    private IToken[] Tokens { get; set; }
    private CardListItem[] Data { get; set; }

    private void RefreshData()
    {
        Data = Tokens.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IToken token)
        => new CardListItem
        {
            Id = token.Id,
            Properties = new[]
            {
                new CardListItemProperty
                {
                    Name = "Id",
                    Value = token.Id.ToString(),
                },
                new CardListItemProperty
                {
                    Name = "CreatedAt",
                    Value = token.CreatedAt.ToString(CultureInfo.InvariantCulture),
                },
                new CardListItemProperty
                {
                    Name = "Name",
                    Value = token.Name,
                },
                new CardListItemProperty
                {
                    Name = "Description",
                    Value = token.Description,
                },
            },
        };

    private async Task OpenCreateTokenDialog()
    {
        await DialogService.OpenAsync<CreateTokenPage>("Create new token",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "700px", Height = "570px", Resizable = true, Draggable = true });
    }
}
