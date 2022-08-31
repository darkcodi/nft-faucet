using CSharpFunctionalExtensions;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Plugins.UploadPlugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class CreateUploadPage : BasicComponent
{
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    [Parameter]
    public IToken Token { get; set; }

    protected override void OnInitialized()
    {
        Data = AppState.Storage.Uploaders.Select(MapCardListItem).ToArray();
    }

    private CardListItem[] Data { get; set; }
    private Guid[] SelectedUploaderIds { get; set; }
    private IUploader SelectedUploader => AppState?.Storage?.Uploaders?.FirstOrDefault(x => x.Id == SelectedUploaderIds?.FirstOrDefault());
    private IReadOnlyCollection<ConfigurationItem> ConfigurationItems { get; set; } = Array.Empty<ConfigurationItem>();
    private Result<Uri>? FileLocation { get; set; }
    private bool ModelIsValid => IsValid();

    private CardListItem MapCardListItem(IUploader uploader)
        => new CardListItem
        {
            Id = uploader.Id,
            ImageLocation = uploader.ImageName != null ? "./images/" + uploader.ImageName : null,
            Header = uploader.Name,
            IsDisabled = !uploader.IsSupported,
            Badges = new[]
            {
                (Settings?.RecommendedUploaders?.Contains(uploader.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !uploader.IsSupported
                    ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"}
                    : null,
            }.Where(x => x != null).ToArray(),
        };

    private async Task OnChange(int pageNumber)
    {
        if (pageNumber == 1)
        {
            ConfigurationItems = SelectedUploader.GetConfigurationItems();
            StateHasChangedSafe();
        }
        else if (pageNumber == 2)
        {
            await Upload();
        }
    }

    private async Task Upload()
    {
        FileLocation = await SelectedUploader.Upload(Token);
        if (FileLocation.HasValue)
        {
            if (FileLocation.Value.IsSuccess)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Upload succeeded", FileLocation.Value.Value.OriginalString);
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Upload failed", FileLocation.Value.Error);
            }
        }

        StateHasChangedSafe();
    }

    private async Task VerifyConfiguration()
    {
        var result = await SelectedUploader.TryInitialize(ConfigurationItems);
        if (result.IsSuccess)
        {
            NotificationService.Notify(NotificationSeverity.Success, "Init succeeded", "Configuration is valid");
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, "Init failed", result.Error);
        }
        StateHasChangedSafe();
    }

    private void OnSavePressed()
    {
        if (!IsValid())
            return;

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

    private bool IsValid() => FileLocation != null && FileLocation.Value.IsSuccess;
}
