using System.Text;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models.Enums;
using NftFaucet.Services;

namespace NftFaucet.Pages;

public class Step4Component : BasicComponent
{
    [Inject]
    public IIpfsService IpfsService { get; set; }

    [Inject]
    public IEthereumTransactionService TransactionService { get; set; }

    protected string TransactionHash { get; set; }

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
        if (AppState.Storage.TokenType == TokenType.ERC721)
        {
            TransactionHash = await TransactionService.MintErc721Token(AppState.Metamask.Network!.Value, AppState.Storage.DestinationAddress, AppState.Storage.TokenUrl);
        }
        else
        {
            var amount = (int) AppState.Storage.TokenAmount;
            TransactionHash = await TransactionService.MintErc1155Token(AppState.Metamask.Network!.Value, AppState.Storage.DestinationAddress, amount, AppState.Storage.TokenUrl);
        }

        RefreshMediator.NotifyStateHasChangedSafe();
    }

    protected void Reset()
    {
        AppState.Reset();
        UriHelper.NavigateToRelative("/");
    }
}
