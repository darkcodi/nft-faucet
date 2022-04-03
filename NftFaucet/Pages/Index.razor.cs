using NftFaucet.Components;
using NftFaucet.Extensions;

namespace NftFaucet.Pages;

public class IndexComponent : BasicComponent
{
    protected override async Task OnInitializedAsync()
    {
        UriHelper.NavigateToRelative(await Metamask.IsReady() ? "/step1" : "/connect-metamask");
    }
}
