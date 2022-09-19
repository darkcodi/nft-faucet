using NftFaucetRadzen.Components;
using NftFaucetRadzen.Components.CardList;
using NftFaucetRadzen.Plugins;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class TokensPage : BasicComponent
{
    protected override void OnInitialized()
    {
        // ToDo: Add loading from IndexedDB
        RefreshCards();
    }

    private CardListItem[] TokenCards { get; set; }

    private void RefreshCards()
    {
        TokenCards = AppState?.Storage?.Tokens?.Select(MapCardListItem).ToArray() ?? Array.Empty<CardListItem>();
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
        var token = (IToken) await DialogService.OpenAsync<CreateTokenPage>("Create new token",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "700px", Height = "570px", Resizable = true, Draggable = true });

        if (token == null)
        {
            return;
        }

        AppState.Storage.Tokens ??= new List<IToken>();
        AppState.Storage.Tokens.Add(token);
        AppState.Storage.SelectedTokens = new[] { token.Id };
        AppState.Storage.SelectedUploadLocations = Array.Empty<Guid>();
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    private void OnTokenChange()
    {
        AppState.Storage.SelectedUploadLocations = Array.Empty<Guid>();
    }
}
