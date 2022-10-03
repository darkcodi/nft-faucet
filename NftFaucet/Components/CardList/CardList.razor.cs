using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NftFaucet.Pages;
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

    private void ShowContextMenu(MouseEventArgs args, CardListItemButton[] contextMenuButtons)
    {
        if (contextMenuButtons == null || contextMenuButtons.Length == 0)
            return;

        ContextMenuService.Open(args,
            contextMenuButtons.Select(x => new ContextMenuItem
            {
                Text = x.Name,
                Value = x.Action,
            }).ToList(), OnMenuItemClick);
    }

    private void OnMenuItemClick(MenuItemEventArgs obj)
    {
        ContextMenuService.Close();
        var action = (Action) obj.Value;
        action();
    }
}
