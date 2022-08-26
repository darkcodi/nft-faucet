using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Components;

public partial class CardList
{
    [Parameter] public CardListItem[] Data { get; set; }
    [Parameter] public Guid[] SelectedItemIds { get; set; }
    [Parameter] public EventCallback<Guid[]> SelectedItemIdsChanged { get; set; }
    [Parameter] public bool AllowMultipleSelection { get; set; }
    [Parameter] public bool AllowUnselect { get; set; }

    public async Task ToggleSelection(Guid itemId)
    {
        var selectedItemIds = SelectedItemIds?.ToList() ?? new List<Guid>();
        var isAlreadySelected = selectedItemIds.Contains(itemId);
        if (isAlreadySelected && AllowUnselect)
        {
            selectedItemIds.Remove(itemId);
        }
        else if (!isAlreadySelected)
        {
            if (!AllowMultipleSelection)
            {
                selectedItemIds.Clear();
            }
            selectedItemIds.Add(itemId);
        }
        SelectedItemIds = selectedItemIds.ToArray();
        await SelectedItemIdsChanged.InvokeAsync(SelectedItemIds);
        StateHasChanged();
    }
}
