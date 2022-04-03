using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models;

namespace NftFaucet.Pages;

public class Step3Component : BasicComponent
{
    protected string TokenUrlErrorMessage { get; set; }
    protected string DestinationAddressErrorMessage { get; set; }
    protected string TokenUrlClass => string.IsNullOrWhiteSpace(TokenUrlErrorMessage) ? null : "invalid-input";
    protected string DestinationAddressClass => string.IsNullOrWhiteSpace(DestinationAddressErrorMessage) ? null : "invalid-input";

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady() || string.IsNullOrEmpty(AppState.Storage.TokenUrl))
            UriHelper.NavigateToRelative("/");

        AppState.Navigation.SetForwardHandler(ForwardHandler);
        AppState.Storage.DestinationAddress = AppState.Metamask.Address;
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
        var isValidDestinationAddress = Address.Create(AppState.Storage.DestinationAddress).IsSuccess;

        if (!isValidTokenUri)
        {
            TokenUrlErrorMessage = "Invalid token URI";
        }

        if (!isValidDestinationAddress)
        {
            DestinationAddressErrorMessage = "Invalid destination address";
        }

        RefreshMediator.NotifyStateHasChangedSafe();

        return Task.FromResult(isValidTokenUri && isValidDestinationAddress);
    }
}
