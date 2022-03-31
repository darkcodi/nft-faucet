using NftFaucet.Components;

namespace NftFaucet.Shared;

public class MainLayoutComponent : LayoutBasicComponent
{
    private const int StepsCount = 4;

    protected string Subtitle => $"Address: {AppState?.Metamask?.Address ?? "<null>"}; Chain: {AppState?.Metamask?.Network?.ToString() ?? "<unknown>"} ({AppState?.Metamask?.ChainId.ToString() ?? "<null>"})";

    protected bool IsFirstStep => AppState.Navigation.CurrentStep == 1;
    protected bool IsLastStep => AppState.Navigation.CurrentStep == StepsCount;
    protected string BackButtonStyle => $"visibility: {(IsFirstStep || IsLastStep ? "hidden" : "visible")}";
    protected string ForwardButtonStyle => $"visibility: {(IsLastStep ? "hidden" : "visible")}";

    protected string ForwardButtonText => AppState.Navigation.CurrentStep switch
    {
        1 => "Review NFT",
        2 => "Review mint",
        3 => "Send me this NFT!",
        _ => "Next"
    };

    protected string ForwardButtonIcon => AppState.Navigation.CurrentStep switch
    {
        3 => "send",
        _ => "arrow-right",
    };
}
