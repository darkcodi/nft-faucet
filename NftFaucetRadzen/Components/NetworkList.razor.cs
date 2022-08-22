using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Components;

public partial class NetworkList
{
    [Parameter] public NetworkModel[] Data { get; set; }
    [Parameter] public Guid[] SelectedNetworkIds { get; set; }
    [Parameter] public bool AllowMultipleSelection { get; set; }
    [Parameter] public bool AllowUnselect { get; set; }

    public void ToggleSelection(Guid networkId)
    {
        var selectedNetworks = SelectedNetworkIds?.ToList() ?? new List<Guid>();
        var isAlreadySelected = selectedNetworks.Contains(networkId);
        if (isAlreadySelected && AllowUnselect)
        {
            selectedNetworks.Remove(networkId);
        }
        else if (!isAlreadySelected)
        {
            if (!AllowMultipleSelection)
            {
                selectedNetworks.Clear();
            }
            selectedNetworks.Add(networkId);
        }
        SelectedNetworkIds = selectedNetworks.ToArray();
        StateHasChanged();
    }
}
