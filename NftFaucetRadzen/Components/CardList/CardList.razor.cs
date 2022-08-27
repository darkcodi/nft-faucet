using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Components.CardList;

public partial class CardList : BasicComponent
{
    [Parameter] public CardListItem[] Data { get; set; }
    [Parameter] public Guid[] SelectedItems { get; set; }
    [Parameter] public EventCallback<Guid[]> SelectedItemsChanged { get; set; }
    [Parameter] public bool AllowMultipleSelection { get; set; }
    [Parameter] public bool AllowUnselect { get; set; }

    public async Task ToggleSelection(CardListItem item)
    {
        if (item.IsDisabled)
        {
            return;
        }
        
        var selectedItems = SelectedItems?.ToList() ?? new List<Guid>();
        var isAlreadySelected = selectedItems.Contains(item.Id);
        if (isAlreadySelected && AllowUnselect)
        {
            selectedItems.Remove(item.Id);
        }
        else if (!isAlreadySelected)
        {
            if (!AllowMultipleSelection)
            {
                selectedItems.Clear();
            }
            selectedItems.Add(item.Id);
        }
        SelectedItems = selectedItems.ToArray();
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
        RefreshMediator.NotifyStateHasChangedSafe();
    }
}
