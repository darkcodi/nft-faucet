using System.Globalization;
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
        RefreshCards();
    }

    private CardListItem[] UploadCards { get; set; }

    private void RefreshCards()
    {
        UploadCards = AppState?.Storage?.UploadLocations?.Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
    }

    private CardListItem MapCardListItem(ITokenUploadLocation uploadLocation)
        => new CardListItem
        {
            Id = uploadLocation.Id,
            Header = uploadLocation.Name,
            ImageLocation = GetUploaderImageLocation(uploadLocation.UploaderId),
            Properties = new[]
            {
                new CardListItemProperty
                {
                    Name = "Id",
                    Value = uploadLocation.Id.ToString(),
                },
                new CardListItemProperty
                {
                    Name = "Location",
                    Value = uploadLocation.Location,
                },
                new CardListItemProperty
                {
                    Name = "CreatedAt",
                    Value = uploadLocation.CreatedAt.ToString(CultureInfo.InvariantCulture),
                },
            },
        };

    private string GetUploaderImageLocation(Guid uploaderId)
    {
        var uploader = AppState?.Storage?.Uploaders?.FirstOrDefault(x => x.Id == uploaderId);
        if (uploader == null)
        {
            return null;
        }

        return "./images/" + uploader.ImageName;
    }

    private async Task OpenCreateUploadDialog()
    {
        var uploadLocation = (ITokenUploadLocation) await DialogService.OpenAsync<CreateUploadPage>("Create new upload",
            new Dictionary<string, object>
            {
                { "Token", AppState.SelectedToken }, 
            },
            new DialogOptions() { Width = "1000px", Height = "700px", Resizable = true, Draggable = true });
 
        if (uploadLocation == null)
        {
            return;
        }

        AppState.Storage.UploadLocations ??= new List<ITokenUploadLocation>();
        AppState.Storage.UploadLocations.Add(uploadLocation);
        RefreshCards();
        StateHasChangedSafe();
    }
}
