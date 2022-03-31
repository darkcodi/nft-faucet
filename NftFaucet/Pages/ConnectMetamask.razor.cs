using NftFaucet.Components;

namespace NftFaucet.Pages;

public class ConnectMetamaskComponent : BasicComponent
{
    protected override async Task OnInitializedAsync()
    {
        if (await Metamask.IsConnected())
        {
            await Metamask.RefreshAddress();
            UriHelper.NavigateTo("/");
        }
    }

    protected async Task Connect()
    {
        var isConnected = await Metamask.Connect();
        if (isConnected)
        {
            UriHelper.NavigateTo("/");
        }
    }
}
