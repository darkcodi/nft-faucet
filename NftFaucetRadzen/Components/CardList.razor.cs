using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Components;

public partial class CardList : BasicComponent
{
    [Parameter] public CardListItem[] Data { get; set; }
    [Parameter] public CardListItem[] SelectedItems { get; set; }
    [Parameter] public EventCallback<CardListItem[]> SelectedItemsChanged { get; set; }
    [Parameter] public bool AllowMultipleSelection { get; set; }
    [Parameter] public bool AllowUnselect { get; set; }

    public async Task ToggleSelection(CardListItem item)
    {
        if (item.IsDisabled)
        {
            return;
        }
        
        var selectedItems = SelectedItems?.ToList() ?? new List<CardListItem>();
        var isAlreadySelected = selectedItems.Contains(item);
        if (isAlreadySelected && AllowUnselect)
        {
            selectedItems.Remove(item);
        }
        else if (!isAlreadySelected)
        {
            if (!AllowMultipleSelection)
            {
                selectedItems.Clear();
            }
            selectedItems.Add(item);
        }
        SelectedItems = selectedItems.ToArray();
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
        RefreshMediator.NotifyStateHasChangedSafe();
    }
}
