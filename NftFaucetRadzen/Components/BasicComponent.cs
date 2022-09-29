using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Options;
using NftFaucetRadzen.Services;
using Radzen;

namespace NftFaucetRadzen.Components;

public abstract class BasicComponent : ComponentBase
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected ScopedAppState AppState { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected Settings Settings { get; set; }

    [Inject]
    protected StateRepository StateRepository { get; set; }

    protected override void OnInitialized()
    {
        RefreshMediator.StateChanged += async () => await InvokeAsync(StateHasChangedSafe);
    }

    protected void StateHasChangedSafe()
    {
        try
        {
            InvokeAsync(StateHasChanged);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected async Task SaveAppState()
    {
        await StateRepository.SaveAppState(AppState);
    }
}
