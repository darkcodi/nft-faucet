using Microsoft.AspNetCore.Components;
using Radzen;

namespace NftFaucet.Components.CardList;

public partial class CardListItemConfigurationDialog : BasicComponent
{
    [Parameter] public Guid CardListItemId { get; set; }
    [Parameter] public CardListItem CardListItem { get; set; }

    private async Task OnSavePressed()
    {
        var result = await CardListItem.Configuration.ConfigureAction(CardListItem.Configuration.Objects);
        if (result.IsFailure)
        {
            NotificationService.Notify(NotificationSeverity.Error, "Invalid configuration", result.Error);
            return;
        }

        await CardListItem.Configuration.ConfigureAction(CardListItem.Configuration.Objects);
        RefreshMediator.NotifyStateHasChangedSafe();

        DialogService.Close((bool?)true);
    }
}
