using System.Text;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Models;
using NftFaucet.Plugins;
using NftFaucet.Plugins.UploadPlugins;
using Radzen;

namespace NftFaucet.Pages;

public partial class CreateUploadDialog : BasicComponent
{
    [Parameter] public IToken Token { get; set; }

    protected override void OnInitialized()
    {
        RefreshCards();
        base.OnInitialized();
    }

    private CardListItem[] UploaderCards { get; set; }
    private Guid[] SelectedUploaderIds { get; set; }
    private IUploader SelectedUploader => AppState?.PluginStorage?.Uploaders?.FirstOrDefault(x => x.Id == SelectedUploaderIds?.FirstOrDefault());
    private bool IsUploading { get; set; }

    private void RefreshCards()
    {
        UploaderCards = AppState.PluginStorage.Uploaders.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IUploader uploader)
    {
        var configuration = uploader.GetConfiguration();
        return new CardListItem
        {
            Id = uploader.Id,
            ImageLocation = uploader.ImageName != null ? "./images/" + uploader.ImageName : null,
            Header = uploader.Name,
            Properties = uploader.GetProperties(),
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
                    ConfigureAction = async x =>
                    {
                        var result = await configuration.ConfigureAction(x);
                        RefreshCards();
                        if (result.IsSuccess)
                        {
                            await StateRepository.SaveUploaderState(uploader);
                        }
                        return result;
                    },
                },
        };
    }

    private async Task OnSavePressed()
    {
        IsUploading = true;
        RefreshMediator.NotifyStateHasChangedSafe();

        var mainFileLocationResult = await SelectedUploader.Upload(Token.MainFile.FileName, Token.MainFile.FileType, Base64DataToBytes(Token.MainFile.FileData));
        if (mainFileLocationResult.IsFailure)
        {
            IsUploading = false;
            RefreshMediator.NotifyStateHasChangedSafe();
            NotificationService.Notify(NotificationSeverity.Error, "Failed to upload main file", mainFileLocationResult.Error);
            return;
        }
        var mainFileLocation = mainFileLocationResult.Value;

        Uri coverFileLocation = null;
        if (Token.CoverFile != null)
        {
            var coverFileLocationResult = await SelectedUploader.Upload(Token.CoverFile.FileName, Token.CoverFile.FileType, Base64DataToBytes(Token.CoverFile.FileData));
            if (coverFileLocationResult.IsFailure)
            {
                IsUploading = false;
                RefreshMediator.NotifyStateHasChangedSafe();
                NotificationService.Notify(NotificationSeverity.Error, "Failed to upload cover file", coverFileLocationResult.Error);
                return;
            }
            coverFileLocation = coverFileLocationResult.Value;
        }

        var tokenMetadata = GenerateTokenMetadata(Token, mainFileLocation, coverFileLocation);
        var tokenMetadataBytes = Encoding.UTF8.GetBytes(tokenMetadata);
        var tokenLocationResult = await SelectedUploader.Upload($"{Token.Id}.json", "application/json", tokenMetadataBytes);
        if (tokenLocationResult.IsFailure)
        {
            IsUploading = false;
            RefreshMediator.NotifyStateHasChangedSafe();
            NotificationService.Notify(NotificationSeverity.Error, "Failed to upload metadata", tokenLocationResult.Error);
            return;
        }

        IsUploading = false;
        RefreshMediator.NotifyStateHasChangedSafe();
        NotificationService.Notify(NotificationSeverity.Success, "Upload succeeded", tokenLocationResult.Value.OriginalString);
        var uploadLocation = new TokenUploadLocation
        {
            Id = Guid.NewGuid(),
            TokenId = Token.Id,
            Name = SelectedUploader.ShortName,
            Location = tokenLocationResult.Value.OriginalString,
            CreatedAt = DateTime.Now,
            UploaderId = SelectedUploader.Id,
        };
        DialogService.Close(uploadLocation);
    }

    private static byte[] Base64DataToBytes(string fileData)
    {
        var index = fileData.IndexOf(';');
        var encoded = fileData.Substring(index + 8);
        return Convert.FromBase64String(encoded);
    }

    private static string GenerateTokenMetadata(IToken token, Uri mainFileLocation, Uri coverFileLocation)
    {
        var tokenMetadata = new TokenMetadata
        {
            Name = token.Name,
            Description = token.Description,
            Image = coverFileLocation != null ? coverFileLocation.OriginalString : mainFileLocation.OriginalString,
            AnimationUrl = coverFileLocation != null ? mainFileLocation.OriginalString : null,
            ExternalUrl = "https://darkcodi.github.io/nft-faucet/",
        };
        var metadataJson = JsonConvert.SerializeObject(tokenMetadata, Formatting.Indented);
        return metadataJson;
    }
}
