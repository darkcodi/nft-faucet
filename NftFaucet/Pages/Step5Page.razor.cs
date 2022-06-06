using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models.Enums;
using NftFaucet.Services;
using NftFaucet.Utils;

namespace NftFaucet.Pages;

public class Step5Component : BasicComponent
{
    [Inject]
    public IIpfsService IpfsService { get; set; }

    [Inject]
    public IEthereumTransactionService TransactionService { get; set; }

    [Inject]
    public ISolanaTransactionService SolanaTransactionService { get; set; }


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

        if (AppState.Storage.NetworkType == NetworkType.Ethereum && AppState.Storage.TokenType == TokenType.ERC721)
        {
            TransactionHash = await ResultWrapper.Wrap(TransactionService.MintErc721Token(network, address, uri));
        }
        else if (AppState.Storage.NetworkType == NetworkType.Ethereum && AppState.Storage.TokenType == TokenType.ERC1155)
        {
            var amount = (int) AppState.Storage.TokenAmount;
            TransactionHash = await ResultWrapper.Wrap(TransactionService.MintErc1155Token(network, address, amount, uri));
        }
        else if (AppState.Storage.NetworkType == NetworkType.Solana)
        {
            TransactionHash =
                await ResultWrapper.Wrap(SolanaTransactionService.MintNft(AppState.Storage.Network,
                    address, 
                    uri, 
                    AppState.Storage.TokenName, 
                    AppState.Storage.TokenSymbol,
                    AppState.Storage.IsTokenMutable,
                    AppState.Storage.IncludeMasterEdition,
                    (uint)AppState.Storage.SellerFeeBasisPoints,
                    (ulong)AppState.Storage.TokenAmount));
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
        var network = AppState.Metamask.Network;


        if (AppState.Storage.NetworkType == NetworkType.Solana)
        {
            network = AppState.Storage.Network;
        }

        var baseUrl = network switch
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
            EthereumNetwork.SolanaDevnet => "https://explorer.solana.com/tx/",
            EthereumNetwork.SolanaTestnet => "https://explorer.solana.com/tx/",
            EthereumNetwork.SolanaMainnet => "https://explorer.solana.com/tx/",
            _ => null,
        };
        if (baseUrl == null && !network.HasValue)
            return;

        var txHash = TransactionHash!.Value!.Value;
        var txUrl = BuildTxUrl(network.Value, baseUrl, txHash);
        await JsRuntime.InvokeAsync<object>("open", txUrl, "_blank");
    }

    protected async Task RetryTransaction()
    {
        TransactionHash = null;
        RefreshMediator.NotifyStateHasChangedSafe();
        Mint();
    }

    private string BuildTxUrl(EthereumNetwork chain, string baseUrl, string txHash)
    {
        return chain switch
        {
            EthereumNetwork.SolanaDevnet => baseUrl + txHash + "?cluster=devnet",
            EthereumNetwork.SolanaTestnet => baseUrl + txHash + "?cluster=testnet",
            EthereumNetwork.SolanaMainnet => baseUrl + txHash,
            _ => baseUrl + txHash,
        };
    }
}
