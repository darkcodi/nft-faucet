using NftFaucetRadzen.Components;
using NftFaucetRadzen.Extensions;

namespace NftFaucetRadzen.Pages;

public class IndexComponent : BasicComponent
{
    protected override async Task OnInitializedAsync()
    {
        // if (!await Metamask.IsReady())
        // {
        //     UriHelper.NavigateToRelative("/connect-metamask");
        //     return;
        // }
        //
        // if (!AppState.IpfsContext.IsInitialized)
        // {
        //     UriHelper.NavigateToRelative("/connect-ipfs");
        //     return;
        // }

        UriHelper.NavigateToRelative("/network");
    }
}
