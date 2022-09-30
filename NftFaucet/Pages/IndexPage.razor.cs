using NftFaucet.Components;
using NftFaucet.Extensions;

namespace NftFaucet.Pages;

public partial class IndexPage : BasicComponent
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

        NavigationManager.NavigateToRelative("/networks");
    }
}
