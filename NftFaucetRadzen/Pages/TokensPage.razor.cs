using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Models;
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
        RefreshData();
    }

    private CardListItem[] Data { get; set; }

    private void RefreshData()
    {
        Data = AppState?.Storage?.Tokens?.Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
    }

    private CardListItem MapCardListItem(IToken token)
        => new CardListItem
        {
            Id = token.Id,
            Header = token.Name,
            ImageLocation = token.Image.FileData,
            Properties = new[]
            {
                new CardListItemProperty
                {
                    Name = "Description",
                    Value = token.Description,
                },
            },
        };

    private async Task OpenCreateTokenDialog()
    {
        var newFileModel = (NewFileModel) await DialogService.OpenAsync<CreateTokenPage>("Create new token",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "700px", Height = "570px", Resizable = true, Draggable = true });
        
        var token = new Token
        {
            Id = Guid.NewGuid(),
            Name = newFileModel.Name,
            Description = newFileModel.Description,
            CreatedAt = DateTime.Now,
            Image = new TokenMedia
            {
                FileName = newFileModel.FileName,
                FileSize = newFileModel.FileSize!.Value,
                FileData = newFileModel.FileData,
            },
        };
        AppState.Storage.Tokens ??= new List<IToken>();
        AppState.Storage.Tokens.Add(token);
        RefreshData();
        StateHasChangedSafe();
    }
}
