using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Shared;

public partial class NavMenu : BasicComponent
{
    protected string SelectedNetworkName => AppState.Storage.SelectedNetworks?.FirstOrDefault()?.Header;
}
