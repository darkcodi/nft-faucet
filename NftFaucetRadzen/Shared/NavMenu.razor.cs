using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Shared;

public partial class NavMenu : BasicComponent
{
    protected Guid? SelectedNetworkId => AppState?.Storage?.SelectedNetworks?.FirstOrDefault();
    protected string SelectedNetworkName => PluginLoader?.NetworkPlugins?.SelectMany(x => x?.Networks).FirstOrDefault(x => x?.Id == SelectedNetworkId)?.ShortName;
    protected Guid? SelectedProviderId => AppState?.Storage?.SelectedProviders?.FirstOrDefault();
    protected string SelectedProviderName => PluginLoader?.ProviderPlugins?.SelectMany(x => x?.Providers).FirstOrDefault(x => x?.Id == SelectedProviderId)?.ShortName;

    private bool CollapseNavMenu { get; set; } = true;

    private string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        CollapseNavMenu = !CollapseNavMenu;
    }
}
