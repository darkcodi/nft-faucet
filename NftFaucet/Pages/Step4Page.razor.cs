using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models;
using NftFaucet.Models.Enums;
using Solnet.Wallet;

namespace NftFaucet.Pages;

public class Step4Component : BasicComponent
{
    protected string TokenUrlErrorMessage { get; set; }
    protected string DestinationAddressErrorMessage { get; set; }
    protected string TokenUrlClass => string.IsNullOrWhiteSpace(TokenUrlErrorMessage) ? null : "invalid-input";
    protected string DestinationAddressClass => string.IsNullOrWhiteSpace(DestinationAddressErrorMessage) ? null : "invalid-input";
    protected bool IsSupportedNetwork => AppState?.Metamask?.Network != null && Settings?.GetEthereumNetworkOptions(AppState.Metamask.Network!.Value) != null;

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady() || !AppState.IpfsContext.IsInitialized || string.IsNullOrEmpty(AppState.Storage.TokenUrl))
            UriHelper.NavigateToRelative("/");

        AppState.Navigation.SetForwardHandler(ForwardHandler);
        AppState.Storage.DestinationAddress = AppState.Storage.NetworkType == NetworkType.Solana
                ? "51CNhAWJ94HrvXLNJrbXzhzgSixpwvwYvXTA9U6itENE"
                : AppState.Metamask.Address;
    }

    protected void OnTokenUrlInputChange()
    {
        TokenUrlErrorMessage = string.Empty;
    }

    protected void OnDestinationAddressInputChange()
    {
        DestinationAddressErrorMessage = string.Empty;
    }

    protected Task<bool> ForwardHandler()
    {
        var isValidTokenUri = !string.IsNullOrWhiteSpace(AppState.Storage.TokenUrl);
        var isValidDestinationAddress = AppState.Storage.NetworkType == NetworkType.Ethereum
            ? Address.Create(AppState.Storage.DestinationAddress).IsSuccess
            : SolanaAddress.Create(AppState.Storage.DestinationAddress).IsSuccess;

        if (!isValidTokenUri)
        {
            TokenUrlErrorMessage = "Invalid token URI";
        }

        if (!isValidDestinationAddress)
        {
            DestinationAddressErrorMessage = "Invalid destination address";
        }

        RefreshMediator.NotifyStateHasChangedSafe();

        return Task.FromResult(isValidTokenUri && isValidDestinationAddress && IsSupportedNetwork);
    }
}
