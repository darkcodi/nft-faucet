using NftFaucet.Components;
using NftFaucet.Models.Enums;

namespace NftFaucet.Pages;

public class Step1Component : BasicComponent
{
    protected EnumWrapper<NetworkType>[] NetworkTypes { get; } = Enum.GetValues<NetworkType>()
        .Select(x => new EnumWrapper<NetworkType>(x, x.ToString())).ToArray();

    protected EnumWrapper<EthereumNetwork>[] ChainTypes { get; } = new List<EthereumNetwork>() {EthereumNetwork.SolanaTestnet, EthereumNetwork.SolanaDevnet, EthereumNetwork.SolanaMainnet}
        .Select(x => new EnumWrapper<EthereumNetwork>(x, x.ToString())).ToArray();


    protected void OnEthereumSelected()
    {
        AppState.Storage.NetworkType = NetworkType.Ethereum;

        AppState.Navigation.GoForward();
    }

    protected void OnSolanaSelected()
    {
        AppState.Storage.NetworkType = NetworkType.Solana;
    }

    protected void OnNetworkChange(EnumWrapper<EthereumNetwork> network)
    {
        AppState.Storage.Network = network.Value;
        RefreshMediator.NotifyStateHasChangedSafe();

        AppState.Navigation.GoForward();
    }
}
