using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Shared;

public partial class NavMenu : BasicComponent
{
    protected Guid? SelectedNetworkId => AppState?.Storage?.SelectedNetworks?.FirstOrDefault();
    protected string SelectedNetworkName => AppState?.Storage?.Networks?.FirstOrDefault(x => x?.Id == SelectedNetworkId)?.ShortName;
    protected Guid? SelectedProviderId => AppState?.Storage?.SelectedProviders?.FirstOrDefault();
    protected string SelectedProviderName => AppState?.Storage?.Providers?.FirstOrDefault(x => x?.Id == SelectedProviderId)?.ShortName;
    protected Guid? SelectedContractId => AppState?.Storage?.SelectedContracts?.FirstOrDefault();
    protected string SelectedContractName => AppState?.Storage?.Contracts?.FirstOrDefault(x => x?.Id == SelectedContractId)?.Symbol;

    private bool CollapseNavMenu { get; set; } = true;

    private string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        CollapseNavMenu = !CollapseNavMenu;
    }
}
