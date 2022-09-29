using System.Globalization;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class UploadLocationsPage : BasicComponent
{
    protected override void OnInitialized()
    {
        RefreshCards();
    }

    private CardListItem[] UploadCards { get; set; }

    private void RefreshCards()
    {
        var selectedTokenId = AppState?.UserStorage?.SelectedTokens?.FirstOrDefault();
        UploadCards = AppState?.UserStorage?.UploadLocations?.Where(x => x.TokenId == selectedTokenId).Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
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
                    Link = uploadLocation.Location,
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
        var uploader = AppState?.PluginStorage?.Uploaders?.FirstOrDefault(x => x.Id == uploaderId);
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

        AppState.UserStorage.UploadLocations ??= new List<ITokenUploadLocation>();
        AppState.UserStorage.UploadLocations.Add(uploadLocation);
        AppState.UserStorage.SelectedUploadLocations = new[] { uploadLocation.Id };
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
        await StateRepository.SaveUploadLocation(uploadLocation);
        await SaveAppState();
    }

    private async Task OnUploadLocationChange()
    {
        await SaveAppState();
    }
}
