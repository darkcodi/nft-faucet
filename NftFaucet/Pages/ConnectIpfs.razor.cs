using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Utils;

namespace NftFaucet.Pages;

public class ConnectIpfsComponent : BasicComponent
{
    [Inject]
    protected IJSRuntime JsRuntime { get; set; }

    protected string TitleText => "In order to use Crust (IPFS provider), please sign the message";
    protected string ButtonText => "Sign message";

    protected override async Task OnInitializedAsync()
    {
        if (await Metamask.IsConnected())
        {
            await Metamask.RefreshAddress();
        }

        if (AppState.IpfsContext.IsInitialized)
        {
            UriHelper.NavigateToRelative("/");
        }
    }

    protected async Task Sign()
    {
        var address = Metamask.Address.ToLowerInvariant();
        var signedMessageResult = await ResultWrapper.Wrap(Metamask.Service.SignAsync(address));
        if (signedMessageResult.IsFailure)
        {
            return;
        }

        var signedMessage = signedMessageResult.Value;
        AppState.IpfsContext.Initialize(address, signedMessage);
        UriHelper.NavigateToRelative("/");
    }
}
