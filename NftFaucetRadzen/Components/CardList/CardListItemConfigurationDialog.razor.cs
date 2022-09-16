using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MimeTypes;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Services;
using Radzen;

namespace NftFaucetRadzen.Components.CardList;

public partial class CardListItemConfigurationDialog
{
    [Parameter]
    public Guid CardListItemId { get; set; }

    [Parameter]
    public CardListItem CardListItem { get; set; }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

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
