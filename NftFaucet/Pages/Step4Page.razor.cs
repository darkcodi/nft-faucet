using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models.Enums;
using NftFaucet.Services;
using NftFaucet.Utils;

namespace NftFaucet.Pages;

public class Step4Component : BasicComponent
{
    [Inject]
    public IIpfsService IpfsService { get; set; }

    [Inject]
    public IEthereumTransactionService TransactionService { get; set; }

    [Inject]
    protected IJSRuntime JsRuntime { get; set; }

    protected Result<string>? TransactionHash { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady() || !AppState.IpfsContext.IsInitialized || string.IsNullOrEmpty(AppState.Storage.DestinationAddress))
        {
            UriHelper.NavigateToRelative("/");
        }
        else
        {
            Task.Run(Mint);
        }
    }

    public async Task Mint()
    {
        var network = AppState.Metamask.Network!.Value;
        var address = AppState.Storage.DestinationAddress;
        var uri = AppState.Storage.TokenUrl;

        if (AppState.Storage.TokenType == TokenType.ERC721)
        {
            TransactionHash = await ResultWrapper.Wrap(TransactionService.MintErc721Token(network, address, uri));
        }
        else
        {
            var amount = (int) AppState.Storage.TokenAmount;
            TransactionHash = await ResultWrapper.Wrap(TransactionService.MintErc1155Token(network, address, amount, uri));
        }

        RefreshMediator.NotifyStateHasChangedSafe();
    }

    protected void ResetState()
    {
        AppState.Reset();
        UriHelper.NavigateToRelative("/");
    }

    protected async Task ViewOnExplorer()
    {
        var baseUrl = AppState.Metamask.Network switch
        {
            EthereumNetwork.EthereumMainnet => "https://etherscan.io/tx/",
            EthereumNetwork.Ropsten => "https://ropsten.etherscan.io/tx/",
            EthereumNetwork.Rinkeby => "https://rinkeby.etherscan.io/tx/",
            EthereumNetwork.Goerli => "https://goerli.etherscan.io/tx/",
            EthereumNetwork.Kovan => "https://kovan.etherscan.io/tx/",
            EthereumNetwork.OptimismMainnet => "https://optimistic.etherscan.io/tx/",
            EthereumNetwork.OptimismKovan => "https://kovan-optimistic.etherscan.io/tx/",
            EthereumNetwork.PolygonMainnet => "https://polygonscan.com/tx/",
            EthereumNetwork.PolygonMumbai => "https://mumbai.polygonscan.com/tx/",
            EthereumNetwork.MoonbeamMainnet => "https://blockscout.moonbeam.network/tx/",
            EthereumNetwork.MoonbaseAlpha => "https://moonbase.moonscan.io/tx/",
            EthereumNetwork.ArbitrumMainnetBeta => "https://explorer.arbitrum.io/tx/",
            EthereumNetwork.ArbitrumRinkeby => "https://testnet.arbiscan.io/tx/",
            EthereumNetwork.ArbitrumGoerli => "https://nitro-devnet-explorer.arbitrum.io/tx/",
            EthereumNetwork.AvalancheMainnet => "https://snowtrace.io/tx/",
            EthereumNetwork.AvalancheFuji => "https://testnet.snowtrace.io/tx/",
            _ => null,
        };
        if (baseUrl == null)
            return;

        var txHash = TransactionHash!.Value!.Value;
        var txUrl = baseUrl + txHash;
        await JsRuntime.InvokeAsync<object>("open", txUrl, "_blank");
    }

    protected async Task RetryTransaction()
    {
        TransactionHash = null;
        RefreshMediator.NotifyStateHasChangedSafe();
        Mint();
    }
}
