using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Plugins.UploadPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class CreateUploadPage : BasicComponent
{
    [Parameter] public IToken Token { get; set; }

    protected override void OnInitialized()
    {
        RefreshCards();
        base.OnInitialized();
    }

    private CardListItem[] UploaderCards { get; set; }
    private Guid[] SelectedUploaderIds { get; set; }
    private IUploader SelectedUploader => AppState?.Storage?.Uploaders?.FirstOrDefault(x => x.Id == SelectedUploaderIds?.FirstOrDefault());
    private Result<Uri>? FileLocation { get; set; }

    private void RefreshCards()
    {
        UploaderCards = AppState.Storage.Uploaders.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IUploader uploader)
    {
        var configuration = uploader.GetConfiguration();
        return new CardListItem
        {
            Id = uploader.Id,
            ImageLocation = uploader.ImageName != null ? "./images/" + uploader.ImageName : null,
            Header = uploader.Name,
            Properties = uploader.GetProperties().Select(x => new CardListItemProperty
            {
                Name = x.Name,
                Value = x.Value,
            }).ToArray(),
            IsDisabled = !uploader.IsSupported,
            SelectionIcon = uploader.IsConfigured ? CardListItemSelectionIcon.Checkmark : CardListItemSelectionIcon.Warning,
            Badges = new[]
            {
                (Settings?.RecommendedUploaders?.Contains(uploader.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !uploader.IsSupported
                    ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"}
                    : null,
            }.Where(x => x != null).ToArray(),
            Configuration = configuration == null
                ? null
                : new CardListItemConfiguration
                {
                    Objects = configuration.Objects,
                    ConfigureAction = x =>
                    {
                        var result = configuration.ConfigureAction(x);
                        RefreshCards();
                        return result;
                    },
                },
        };
    }

    private async Task OnSavePressed()
    {
        FileLocation = await SelectedUploader.Upload(Token);
        if (FileLocation.Value.IsSuccess)
        {
            NotificationService.Notify(NotificationSeverity.Success, "Upload succeeded", FileLocation.Value.Value.OriginalString);
            var uploadLocation = new TokenUploadLocation
            {
                Id = Guid.NewGuid(),
                Name = SelectedUploader.ShortName,
                Location = FileLocation!.Value!.Value!.OriginalString,
                CreatedAt = DateTime.Now,
                UploaderId = SelectedUploader.Id,
            };
            DialogService.Close(uploadLocation);
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, "Upload failed", FileLocation.Value.Error);
        }
    }
}
