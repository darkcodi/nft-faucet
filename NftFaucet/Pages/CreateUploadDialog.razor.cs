using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Domain.Services;
using NftFaucet.Domain.Utils;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using Radzen;

#pragma warning disable CS8974

namespace NftFaucet.Pages;

public partial class CreateUploadDialog : BasicComponent
{
    [Parameter] public IToken Token { get; set; }

    [Inject] public ITokenMetadataGenerator TokenMetadataGenerator { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        RefreshCards();
    }

    private CardListItem[] UploaderCards { get; set; }
    private Guid[] SelectedUploaderIds { get; set; }
    private IUploader SelectedUploader => AppState?.PluginStorage?.Uploaders?.FirstOrDefault(x => x.Id == SelectedUploaderIds?.FirstOrDefault());
    private bool IsUploading { get; set; }

    private void RefreshCards()
    {
        UploaderCards = AppState.PluginStorage.Uploaders.OrderBy(x => x.Order ?? int.MaxValue).Select(MapCardListItem).ToArray();
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    private CardListItem MapCardListItem(IUploader model)
    {
        var configurationItems = model.GetConfigurationItems();
        return new CardListItem
        {
            Id = model.Id,
            ImageLocation = model.ImageName != null ? "./images/" + model.ImageName : null,
            Header = model.Name,
            Properties = model.GetProperties().Select(Map).ToArray(),
            IsDisabled = !model.IsSupported,
            SelectionIcon = model.IsConfigured ? CardListItemSelectionIcon.Checkmark : CardListItemSelectionIcon.Warning,
            Badges = new[]
            {
                (Settings?.RecommendedUploaders?.Contains(model.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !model.IsSupported ? new CardListItemBadge { Style = BadgeStyle.Light, Text = "Not Supported" } : null,
                model.IsDeprecated ? new CardListItemBadge { Style = BadgeStyle.Warning, Text = "Deprecated" } : null,
            }.Where(x => x != null).ToArray(),
            Buttons = configurationItems != null && configurationItems.Any()
                ? new[]
                {
                    new CardListItemButton
                    {
                        Icon = "build",
                        Style = ButtonStyle.Secondary,
                        Action = async () =>
                        {
                            var result = await OpenConfigurationDialog(model);
                            RefreshCards();
                            if (result.IsSuccess)
                            {
                                await StateRepository.SaveUploaderState(model);
                            }
                        }
                    }
                }
                : Array.Empty<CardListItemButton>(),
        };
    }

    private async Task OnSavePressed()
    {
        IsUploading = true;
        RefreshMediator.NotifyStateHasChangedSafe();

        var mainFileLocationResult = await ResultWrapper.Wrap(() => SelectedUploader.Upload(Token.MainFile.FileName, Token.MainFile.FileType, Base64DataToBytes(Token.MainFile.FileData)));
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
            var coverFileLocationResult = await ResultWrapper.Wrap(() => SelectedUploader.Upload(Token.CoverFile.FileName, Token.CoverFile.FileType, Base64DataToBytes(Token.CoverFile.FileData)));
            if (coverFileLocationResult.IsFailure)
            {
                IsUploading = false;
                RefreshMediator.NotifyStateHasChangedSafe();
                NotificationService.Notify(NotificationSeverity.Error, "Failed to upload cover file", coverFileLocationResult.Error);
                return;
            }
            coverFileLocation = coverFileLocationResult.Value;
        }

        var tokenMetadata = TokenMetadataGenerator.GenerateTokenMetadata(Token, mainFileLocation, coverFileLocation);
        var tokenMetadataBytes = Encoding.UTF8.GetBytes(tokenMetadata);
        var tokenLocationResult = await ResultWrapper.Wrap(() => SelectedUploader.Upload($"{Token.Id}.json", "application/json", tokenMetadataBytes));
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

    private async Task<Result> OpenConfigurationDialog(IUploader uploader)
    {
        var configurationItems = uploader.GetConfigurationItems();
        foreach (var configurationItem in configurationItems)
        {
            var prevClickHandler = configurationItem.ClickAction;
            if (prevClickHandler != null)
            {
                configurationItem.ClickAction = () =>
                {
                    prevClickHandler();
                    RefreshMediator.NotifyStateHasChangedSafe();
                };
            }
        }

        var result = (bool?) await DialogService.OpenAsync<ConfigurationDialog>("Configuration",
            new Dictionary<string, object>
            {
                { nameof(ConfigurationDialog.ConfigurationItems), configurationItems },
                { nameof(ConfigurationDialog.ConfigureAction), uploader.Configure },
            },
            new DialogOptions() {Width = "700px", Height = "570px", Resizable = true, Draggable = true});

        return result != null && result.Value ? Result.Success() : Result.Failure("Operation cancelled");
    }

    private static byte[] Base64DataToBytes(string fileData)
    {
        var index = fileData.IndexOf(';');
        var encoded = fileData.Substring(index + 8);
        return Convert.FromBase64String(encoded);
    }

    private CardListItemProperty Map(Property model)
        => model == null ? null : new CardListItemProperty
        {
            Name = model.Name,
            Value = model.Value,
            ValueColor = model.ValueColor,
            Link = model.Link,
        };
}
