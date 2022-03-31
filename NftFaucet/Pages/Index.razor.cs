using NftFaucet.Components;

namespace NftFaucet.Pages;

public class IndexComponent : BasicComponent
{
    protected override async Task OnInitializedAsync()
    {
        UriHelper.NavigateTo(await Metamask.IsReady() ? "/step1" : "/connect-metamask");
    }
}
