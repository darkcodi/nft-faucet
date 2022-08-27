using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Options;
using NftFaucetRadzen.Services;
using NftFaucetRadzen.Services.Abstractions;

namespace NftFaucetRadzen.Components;

public abstract class BasicComponent : ComponentBase
{
    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected ScopedAppState AppState { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

    [Inject]
    protected Settings Settings { get; set; }

    [Inject]
    protected IPluginLoader PluginLoader { get; set; }

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
