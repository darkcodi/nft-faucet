using NftFaucetRadzen.Components;

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
}
