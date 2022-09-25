using CSharpFunctionalExtensions;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Utils;
using Radzen;

namespace NftFaucetRadzen.Pages;

public partial class MintPage : BasicComponent
{
    private string SourceAddress { get; set; }
    private bool NetworkMatches { get; set; }
    private bool BalanceIsZero { get; set; } = true;
    private bool IsReadyToMint => AppState != null &&
                                  AppState.SelectedNetwork != null &&
                                  AppState.SelectedProvider != null &&
                                  AppState.SelectedProvider.IsConfigured &&
                                  AppState.SelectedContract != null &&
                                  AppState.SelectedToken != null &&
                                  AppState.SelectedUploadLocation != null &&
                                  !string.IsNullOrEmpty(SourceAddress) &&
                                  NetworkMatches &&
                                  !BalanceIsZero;

    protected override async Task OnInitializedAsync()
    {
        if (AppState?.SelectedProvider?.IsConfigured ?? false)
        {
            SourceAddress = await ResultWrapper.Wrap(() => AppState.SelectedProvider.GetAddress()).Match(x => x, _ => null);
            AppState.Storage.DestinationAddress = SourceAddress;
            if (string.IsNullOrEmpty(SourceAddress) || AppState.SelectedNetwork == null)
            {
                BalanceIsZero = true;
            }
            else
            {
                var balance = await ResultWrapper.Wrap(() => AppState.SelectedProvider.GetBalance(AppState.SelectedNetwork)).Match(x => x, _ => 0);
                BalanceIsZero = balance == 0;
            }
            if (AppState.SelectedNetwork != null)
            {
                NetworkMatches = await ResultWrapper.Wrap(() => AppState.SelectedProvider.EnsureNetworkMatches(AppState.SelectedNetwork)).Match(x => x, _ => false);
            }
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
