using Microsoft.AspNetCore.Components;
using Radzen;

namespace NftFaucetRadzen.Components.CardList;

public partial class CardListItemConfigurationDialog : BasicComponent
{
    [Parameter] public Guid CardListItemId { get; set; }
    [Parameter] public CardListItem CardListItem { get; set; }

    private async Task OnSavePressed()
    {
        var isValid = await CardListItem.Configuration.ValidationFunc(CardListItem.Configuration.Objects);
        if (!isValid)
        {
            NotificationService.Notify(NotificationSeverity.Error, "Invalid configuration");
            return;
        }

        await CardListItem.Configuration.ConfigureAction(CardListItem.Configuration.Objects);
        RefreshMediator.NotifyStateHasChangedSafe();

        DialogService.Close((bool?)true);
    }
}
