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
                await ResultWrapper.Wrap(SolanaTransactionService.MintNft(AppState.Storage.NetworkChain,
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
            network = AppState.Storage.NetworkChain;
        }

        var baseUrl = network switch
        {
            NetworkChain.EthereumMainnet => "https://etherscan.io/tx/",
            NetworkChain.Ropsten => "https://ropsten.etherscan.io/tx/",
            NetworkChain.Rinkeby => "https://rinkeby.etherscan.io/tx/",
            NetworkChain.Goerli => "https://goerli.etherscan.io/tx/",
            NetworkChain.Kovan => "https://kovan.etherscan.io/tx/",
            NetworkChain.OptimismMainnet => "https://optimistic.etherscan.io/tx/",
            NetworkChain.OptimismKovan => "https://kovan-optimistic.etherscan.io/tx/",
            NetworkChain.PolygonMainnet => "https://polygonscan.com/tx/",
            NetworkChain.PolygonMumbai => "https://mumbai.polygonscan.com/tx/",
            NetworkChain.MoonbeamMainnet => "https://blockscout.moonbeam.network/tx/",
            NetworkChain.MoonbaseAlpha => "https://moonbase.moonscan.io/tx/",
            NetworkChain.ArbitrumMainnetBeta => "https://explorer.arbitrum.io/tx/",
            NetworkChain.ArbitrumRinkeby => "https://testnet.arbiscan.io/tx/",
            NetworkChain.ArbitrumGoerli => "https://nitro-devnet-explorer.arbitrum.io/tx/",
            NetworkChain.AvalancheMainnet => "https://snowtrace.io/tx/",
            NetworkChain.AvalancheFuji => "https://testnet.snowtrace.io/tx/",
            NetworkChain.SolanaDevnet => "https://explorer.solana.com/tx/",
            NetworkChain.SolanaTestnet => "https://explorer.solana.com/tx/",
            NetworkChain.SolanaMainnet => "https://explorer.solana.com/tx/",
            NetworkChain.BnbChainMainnet => "https://bscscan.com/tx/",
            NetworkChain.BnbChainTestnet => "https://testnet.bscscan.com/tx/",
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

    private string BuildTxUrl(NetworkChain chain, string baseUrl, string txHash)
    {
        return chain switch
        {
            NetworkChain.SolanaDevnet => baseUrl + txHash + "?cluster=devnet",
            NetworkChain.SolanaTestnet => baseUrl + txHash + "?cluster=testnet",
            NetworkChain.SolanaMainnet => baseUrl + txHash,
            _ => baseUrl + txHash,
        };
    }
}
