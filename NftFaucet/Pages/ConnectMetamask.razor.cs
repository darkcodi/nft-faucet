using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucet.Components;
using NftFaucet.Extensions;

namespace NftFaucet.Pages;

public class ConnectMetamaskComponent : BasicComponent
{
    [Inject]
    protected IJSRuntime JsRuntime { get; set; }

    protected bool HasMetaMask { get; set; }
    protected string TitleText => $"You should {(HasMetaMask ? "connect" : "install")} MetaMask first";
    protected string ButtonText => HasMetaMask ? "Connect" : "Install";

    protected override async Task OnInitializedAsync()
    {
        if (await Metamask.IsConnected())
        {
            await Metamask.RefreshAddress();
            UriHelper.NavigateToRelative("/");
        }

        HasMetaMask = Metamask.HasMetaMask ?? false;
    }

    protected async Task Connect()
    {
        if (!HasMetaMask)
        {
            string url = "https://metamask.io/download/";
            await JsRuntime.InvokeAsync<object>("open", url, "_blank");
            return;
        }

        var isConnected = await Metamask.Connect();
        if (isConnected)
        {
            UriHelper.NavigateToRelative("/");
        }
    }
}
