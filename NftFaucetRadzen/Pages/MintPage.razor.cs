using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;

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
        await AppState.SelectedProvider.Mint(mintRequest);
    }
}
