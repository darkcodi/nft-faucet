using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Plugins.Models;
using Radzen;

namespace NftFaucet.Pages;

public partial class ConfigurationDialog : BasicComponent
{
    [Parameter]
    public ConfigurationItem[] ConfigurationItems { get; set; }

    [Parameter]
    public Func<ConfigurationItem[], Task<Result>> ConfigureAction { get; set; }

    private async Task OnSavePressed()
    {
        var result = await ConfigureAction(ConfigurationItems);
        if (result.IsSuccess)
        {
            RefreshMediator.NotifyStateHasChangedSafe();
            DialogService.Close((bool?)true);
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, "Invalid configuration", result.Error);
        }
    }
}
