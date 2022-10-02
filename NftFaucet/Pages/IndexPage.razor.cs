using NftFaucet.Components;
using NftFaucet.Extensions;

namespace NftFaucet.Pages;

public partial class IndexPage : BasicComponent
{
    protected override async Task OnInitializedAsync()
    {
        NavigationManager.NavigateToRelative("/networks");
    }
}
