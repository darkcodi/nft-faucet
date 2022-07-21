using AntDesign;
using NftFaucet.Components;
using NftFaucet.Models.Enums;

namespace NftFaucet.Shared;

public class MainLayoutComponent : LayoutBasicComponent
{
    private const int StepsCount = 5;

    protected bool IsFirstStep => AppState.Navigation.CurrentStep == 1;
    protected bool IsLastStep => AppState.Navigation.CurrentStep == StepsCount;
    protected string BackButtonStyle => $"visibility: {(IsFirstStep || IsLastStep ? "hidden" : "visible")}";
    protected string ForwardButtonStyle => $"visibility: {(IsLastStep ? "hidden" : "visible")}";

    protected string ForwardButtonText => AppState.Navigation.CurrentStep switch
    {
        1 => "Confirm network selection",
        2 => "Review NFT",
        3 => "Review mint",
        4 => "Send me this NFT!",
        _ => "Next"
    };

    protected string ForwardButtonIcon => AppState.Navigation.CurrentStep switch
    {
        4 => "send",
        _ => "arrow-right",
    };

    protected PresetColor ChainColor => AppState?.Metamask?.Network switch
    {
        NetworkChain.EthereumMainnet => PresetColor.Cyan,
        NetworkChain.Ropsten => PresetColor.Volcano,
        NetworkChain.Rinkeby => PresetColor.Gold,
        NetworkChain.Goerli => PresetColor.GeekBlue,
        NetworkChain.Kovan => PresetColor.Purple,
        NetworkChain.OptimismMainnet => PresetColor.Cyan,
        NetworkChain.OptimismKovan => PresetColor.Purple,
        NetworkChain.PolygonMainnet => PresetColor.Cyan,
        NetworkChain.PolygonMumbai => PresetColor.Pink,
        NetworkChain.MoonbeamMainnet => PresetColor.Cyan,
        NetworkChain.MoonbaseAlpha => PresetColor.Pink,
        NetworkChain.ArbitrumMainnetBeta => PresetColor.Cyan,
        NetworkChain.ArbitrumRinkeby => PresetColor.Gold,
        NetworkChain.ArbitrumGoerli => PresetColor.GeekBlue,
        NetworkChain.AvalancheMainnet => PresetColor.Cyan,
        NetworkChain.AvalancheFuji => PresetColor.Pink,
        NetworkChain.BnbChainMainnet => PresetColor.Orange,
        NetworkChain.BnbChainTestnet => PresetColor.Blue,
        _ => PresetColor.Yellow,
    };

}
