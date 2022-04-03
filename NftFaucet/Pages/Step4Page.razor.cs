using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
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

    protected Result<string>? TransactionHash { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady() || string.IsNullOrEmpty(AppState.Storage.DestinationAddress))
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

    protected void Reset()
    {
        AppState.Reset();
        UriHelper.NavigateToRelative("/");
    }

    protected async Task Retry()
    {
        TransactionHash = null;
        RefreshMediator.NotifyStateHasChangedSafe();
        Mint();
    }
}
