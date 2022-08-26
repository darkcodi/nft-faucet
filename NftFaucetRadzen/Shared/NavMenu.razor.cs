using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Shared;

public partial class NavMenu : BasicComponent
{
    protected Guid? SelectedNetworkId => AppState.Storage.SelectedNetworks?.FirstOrDefault();
    protected string SelectedNetworkName => Settings.Networks.FirstOrDefault(x => x.Id == SelectedNetworkId)?.Name;
}
