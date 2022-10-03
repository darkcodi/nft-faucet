namespace NftFaucet.Components.NavMenu;

public partial class NavMenu : BasicComponent
{
    protected string SelectedNetworkName => AppState?.SelectedNetwork?.ShortName;
    protected string SelectedWalletName => AppState?.SelectedWallet?.ShortName;
    protected string SelectedContractName => AppState?.SelectedContract?.Symbol;
    protected string SelectedTokenName => AppState?.SelectedToken?.Name;
    protected string SelectedUploadName => AppState?.SelectedUploadLocation?.Name;

    private bool CollapseNavMenu { get; set; } = true;

    private string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        CollapseNavMenu = !CollapseNavMenu;
    }
}
