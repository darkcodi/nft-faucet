using AntDesign;
using Microsoft.AspNetCore.Components;
using NftFaucet.Models;
using NftFaucet.Options;
using NftFaucet.Services;

namespace NftFaucet.Components;

public abstract class BasicComponent : ComponentBase
{
    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected ScopedAppState AppState { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

    [Inject]
    protected Settings Settings { get; set; }

    [Inject]
    protected MessageService MessageService { get; set; }

    [Inject]
    protected IIpfsService IpfsService { get; set; }

    protected MetamaskInfo Metamask => AppState?.Metamask;

    protected override void OnInitialized()
    {
        RefreshMediator.StateChanged += async () => await InvokeAsync(StateHasChangedSafe);
    }

    protected void StateHasChangedSafe()
    {
        try
        {
            InvokeAsync(StateHasChanged);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
