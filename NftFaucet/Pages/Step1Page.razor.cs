using NftFaucet.Components;
using NftFaucet.Models.Enums;

namespace NftFaucet.Pages;

public class Step1Component : BasicComponent
{
    protected EnumWrapper<NetworkType>[] NetworkTypes { get; } = Enum.GetValues<NetworkType>()
        .Select(x => new EnumWrapper<NetworkType>(x, x.ToString())).ToArray();

    protected EnumWrapper<NetworkChain>[] ChainTypes { get; } = new List<NetworkChain>() {NetworkChain.SolanaTestnet, NetworkChain.SolanaDevnet, NetworkChain.SolanaMainnet}
        .Select(x => new EnumWrapper<NetworkChain>(x, x.ToString())).ToArray();


    protected void OnEthereumSelected()
    {
        AppState.Storage.NetworkType = NetworkType.Ethereum;

        AppState.Navigation.GoForward();
    }

    protected void OnSolanaSelected()
    {
        AppState.Storage.NetworkType = NetworkType.Solana;
        AppState.Storage.NetworkChain = NetworkChain.SolanaDevnet;
    }

    protected void OnNetworkChange(EnumWrapper<NetworkChain> network)
    {
        AppState.Storage.NetworkChain = network.Value;
        RefreshMediator.NotifyStateHasChangedSafe();

        AppState.Navigation.GoForward();
    }
}
