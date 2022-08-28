using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class CreateTokenPage
{
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    private NewFileModel Model { get; set; } = new NewFileModel();
    private bool ModelIsValid => IsValid();

    private void OnSavePressed()
    {
        if (!IsValid())
            return;

        DialogService.Close(Model);
    }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Model.Name))
            return false;

        if (string.IsNullOrWhiteSpace(Model.Description))
            return false;

        if (string.IsNullOrEmpty(Model.FileData))
            return false;

        if (string.IsNullOrEmpty(Model.FileName))
            return false;

        if (Model.FileSize is null or 0)
            return false;

        return true;
    }
}
