using Microsoft.AspNetCore.Components;
using NftFaucet.Models;

namespace NftFaucet.Components;

public abstract class LayoutBasicComponent : LayoutComponentBase
{
    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected ScopedAppState AppState { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

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
}
