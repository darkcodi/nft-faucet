using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;
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

    [Inject]
    protected PluginLoader PluginLoader { get; set; }

    protected override void OnInitialized()
    {
        PluginLoader.EnsurePluginsLoaded();
        AppState.Storage.Networks = PluginLoader.NetworkPlugins.SelectMany(x => x.Networks).Where(x => x != null).ToArray();
        AppState.Storage.Providers = PluginLoader.ProviderPlugins.SelectMany(x => x.Providers).Where(x => x != null).ToArray();

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
