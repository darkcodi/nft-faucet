using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class MintPage : BasicComponent
{
    private bool IsReadyToMint => AppState != null &&
                                  AppState.SelectedNetwork != null &&
                                  AppState.SelectedProvider != null &&
                                  AppState.SelectedProvider.IsConfigured &&
                                  AppState.SelectedContract != null &&
                                  AppState.SelectedToken != null &&
                                  AppState.SelectedUploadLocation != null;

    protected override async Task OnInitializedAsync()
    {
        if (AppState?.SelectedProvider?.IsConfigured ?? false)
        {
            AppState.Storage.DestinationAddress = await AppState.SelectedProvider.GetAddress();
        }
    }

    private async Task Mint()
    {
        var mintRequest = new MintRequest(AppState.SelectedNetwork, AppState.SelectedProvider,
            AppState.SelectedContract, AppState.SelectedToken, AppState.SelectedUploadLocation,
            AppState.Storage.DestinationAddress, AppState.Storage.TokenAmount);
        var result = await AppState.SelectedProvider.Mint(mintRequest);
        if (result.IsSuccess)
        {
            NotificationService.Notify(NotificationSeverity.Success, "Minting finished", result.Value);
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, "Failed to mint", result.Error);
        }
    }
}
