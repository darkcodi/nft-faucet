using Microsoft.AspNetCore.Components;
using Radzen;

namespace NftFaucet.Components.CardList;

public partial class CardList : BasicComponent
{
    [Parameter] public CardListItem[] Data { get; set; }
    [Parameter] public Guid[] SelectedItems { get; set; }
    [Parameter] public EventCallback<Guid[]> SelectedItemsChanged { get; set; }
    [Parameter] public EventCallback<Guid[]> OnSelectedChange { get; set; }
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
        await OnSelectedChange.InvokeAsync(SelectedItems);
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    protected async Task OpenItemConfigurationDialog(CardListItem item)
    {
        var result = (bool?) await DialogService.OpenAsync<CardListItemConfigurationDialog>("Configuration",
            new Dictionary<string, object>
            {
                { "CardListItemId", item.Id },
                { "CardListItem", item },
            },
            new DialogOptions() {Width = "700px", Height = "570px", Resizable = true, Draggable = true});

        if (result != null && result.Value)
        {
            RefreshMediator.NotifyStateHasChangedSafe();
        }
    }
}
