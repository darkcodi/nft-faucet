using ByteSizeLib;
using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Plugins;
using Radzen;

namespace NftFaucet.Pages;

public partial class TokensPage : BasicComponent
{
    protected override void OnInitialized()
    {
        RefreshCards();
    }

    private CardListItem[] TokenCards { get; set; }

    private void RefreshCards()
    {
        TokenCards = AppState?.UserStorage?.Tokens?.Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
    }

    private CardListItem MapCardListItem(IToken token)
    {
        var properties = new List<CardListItemProperty>
        {
            new CardListItemProperty
            {
                Name = "Description",
                Value = token.Description,
            },
        };
        properties.Add(new CardListItemProperty
        {
            Name = token.CoverFile == null ? "Size" : "MF Size",
            Value = ByteSize.FromBytes(token.MainFile.FileSize).ToString(),
        });
        if (token.CoverFile != null)
        {
            properties.Add(new CardListItemProperty
            {
                Name = "CF Size",
                Value = ByteSize.FromBytes(token.CoverFile.FileSize).ToString(),
            });
        }
        return new CardListItem
        {
            Id = token.Id,
            Header = token.Name,
            ImageLocation = token.CoverFile?.FileData ?? token.MainFile.FileData,
            Properties = properties.ToArray(),
        };
    }

    private async Task OpenCreateTokenDialog()
    {
        var token = (IToken) await DialogService.OpenAsync<CreateTokenDialog>("Create new token",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "700px", Height = "570px", Resizable = true, Draggable = true });

        if (token == null)
        {
            return;
        }

        AppState.UserStorage.Tokens ??= new List<IToken>();
        AppState.UserStorage.Tokens.Add(token);
        AppState.UserStorage.SelectedTokens = new[] { token.Id };
        AppState.UserStorage.SelectedUploadLocations = Array.Empty<Guid>();
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
        await StateRepository.SaveToken(token);
        await SaveAppState();
    }

    private async Task OnTokenChange()
    {
        AppState.UserStorage.SelectedUploadLocations = Array.Empty<Guid>();
        await SaveAppState();
    }
}
