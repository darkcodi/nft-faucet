using AntDesign;
using NftFaucet.Components;
using NftFaucet.Models.Enums;

namespace NftFaucet.Shared;

public class MainLayoutComponent : LayoutBasicComponent
{
    private const int StepsCount = 4;

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

    protected PresetColor ChainColor => AppState?.Metamask?.Network switch
    {
        EthereumNetwork.EthereumMainnet => PresetColor.Cyan,
        EthereumNetwork.Ropsten => PresetColor.Volcano,
        EthereumNetwork.Rinkeby => PresetColor.Gold,
        EthereumNetwork.Goerli => PresetColor.GeekBlue,
        EthereumNetwork.Kovan => PresetColor.Purple,
        EthereumNetwork.OptimismMainnet => PresetColor.Cyan,
        EthereumNetwork.OptimismKovan => PresetColor.Red,
        EthereumNetwork.PolygonMainnet => PresetColor.Cyan,
        EthereumNetwork.PolygonMumbai => PresetColor.Pink,
        _ => PresetColor.Yellow,
    };

}
