using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Extensions;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Services;

namespace NftFaucetRadzen.Components;

public abstract class BasicLayout : LayoutComponentBase
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
