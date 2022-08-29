using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class UploadLocationsPage : BasicComponent
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
        Data = AppState?.Storage?.UploadLocations?.Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
    }

    private CardListItem MapCardListItem(ITokenUploadLocation uploadLocation)
        => new CardListItem
        {
            Id = uploadLocation.Id,
            Header = uploadLocation.Name,// $"{uploadLocation.StorageType} ({uploadLocation.UploadProvider})",
            // ToDo: Add image of upload location type
            // ImageLocation = uploadLocation.Image.FileData,
            Properties = new[]
            {
                new CardListItemProperty
                {
                    Name = "Id",
                    Value = uploadLocation.Id.ToString(),
                },
            },
        };

    private async Task OpenCreateUploadDialog()
    {
        await DialogService.OpenAsync<CreateUploadPage>("Create new upload",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "1000px", Height = "700px", Resizable = true, Draggable = true });
        //
        // var token = new Token
        // {
        //     Id = Guid.NewGuid(),
        //     Name = newFileModel.Name,
        //     Description = newFileModel.Description,
        //     CreatedAt = DateTime.Now,
        //     Image = new TokenMedia
        //     {
        //         FileName = newFileModel.FileName,
        //         FileSize = newFileModel.FileSize!.Value,
        //         FileData = newFileModel.FileData,
        //     },
        // };
        // AppState.Storage.Tokens ??= new List<IToken>();
        // AppState.Storage.Tokens.Add(token);
        // RefreshData();
        // StateHasChangedSafe();
    }
}
