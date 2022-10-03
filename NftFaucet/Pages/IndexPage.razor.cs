using NftFaucet.Components;
using NftFaucet.Extensions;

namespace NftFaucet.Pages;

public partial class IndexPage : BasicComponent
{
    protected override Task OnInitializedAsync()
    {
        NavigationManager.NavigateToRelative("/networks");
        return Task.CompletedTask;
    }
}
