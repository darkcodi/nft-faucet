using System.Numerics;
using NftFaucet.Components;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Utils;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using Radzen;

#pragma warning disable CS4014

namespace NftFaucet.Pages;

public partial class MintDialog : BasicComponent
{
    private const int MinDelayInMilliseconds = 300;

    private string ProgressBarText { get; set; } = "Checking network...";
    private MintingState State { get; set; } = MintingState.CheckingNetwork;
    private INetwork WalletNetwork { get; set; }
    private string SourceAddress { get; set; }
    private Balance Balance { get; set; }
    private string TransactionHash { get; set; }
    private string GetBalanceError { get; set; }
    private string SendTransactionError { get; set; }

    protected override Task OnInitializedAsync()
    {
        CheckNetwork();
        return Task.CompletedTask;
    }

    private async Task CheckNetwork()
    {
        State = MintingState.CheckingNetwork;
        ProgressBarText = "Checking network...";
        RefreshMediator.NotifyStateHasChangedSafe();

        var walletNetworkResult = await ResultWrapper.Wrap(async () =>
        {
            var task1 = AppState.SelectedWallet.GetNetwork(AppState.PluginStorage.Networks.ToArray(), AppState.SelectedNetwork);
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        WalletNetwork = walletNetworkResult.IsSuccess ? walletNetworkResult.Value : null;

        if (WalletNetwork?.Id == AppState.SelectedNetwork.Id)
        {
            CheckAddress();
        }
        else
        {
            ProgressBarText = null;
            RefreshMediator.NotifyStateHasChangedSafe();
        }
    }

    private async Task CheckAddress()
    {
        State = MintingState.CheckingAddress;
        ProgressBarText = "Checking address...";
        RefreshMediator.NotifyStateHasChangedSafe();

        var sourceAddressResult = await ResultWrapper.Wrap(async () =>
        {
            var task1 = AppState.SelectedWallet.GetAddress();
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        SourceAddress = sourceAddressResult.IsSuccess ? sourceAddressResult.Value : null;

        if (!string.IsNullOrEmpty(SourceAddress))
        {
            CheckBalance();
        }
        else
        {
            ProgressBarText = null;
            RefreshMediator.NotifyStateHasChangedSafe();
        }
    }

    private async Task CheckBalance()
    {
        State = MintingState.CheckingBalance;
        ProgressBarText = "Checking balance...";
        RefreshMediator.NotifyStateHasChangedSafe();

        var balanceResult = await ResultWrapper.Wrap(async () =>
        {
            var task1 = AppState.SelectedWallet.GetBalance(AppState.SelectedNetwork);
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        Balance = balanceResult.IsSuccess ? balanceResult.Value : null;
        GetBalanceError = balanceResult.IsFailure ? balanceResult.Error : null;
        var amount = Balance?.Amount ?? BigInteger.Zero;
        if (amount < BigInteger.Zero)
        {
            amount = BigInteger.Zero;
        }

        if (amount != BigInteger.Zero)
        {
            SendTransaction();
        }
        else
        {
            ProgressBarText = null;
            RefreshMediator.NotifyStateHasChangedSafe();
        }
    }

    private async Task SendTransaction()
    {
        State = MintingState.SendingTransaction;
        ProgressBarText = "Sending transaction...";
        RefreshMediator.NotifyStateHasChangedSafe();

        var sendTransactionResult = await ResultWrapper.Wrap(async () =>
        {
            var mintRequest = new MintRequest(AppState.SelectedNetwork, AppState.SelectedWallet,
                AppState.SelectedContract, AppState.SelectedToken, AppState.SelectedUploadLocation,
                AppState.UserStorage.DestinationAddress, AppState.UserStorage.TokenAmount);
            var task1 = AppState.SelectedWallet.Mint(mintRequest);
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        if (sendTransactionResult.IsSuccess)
        {
            if (!string.IsNullOrEmpty(sendTransactionResult.Value))
            {
                TransactionHash = sendTransactionResult.Value;
                State = MintingState.Done;
            }
            else
            {
                SendTransactionError = "Tx hash is null or empty";
            }
        }
        else
        {
            SendTransactionError = sendTransactionResult.Error;
        }

        ProgressBarText = null;
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    private void Close()
    {
        DialogService.Close(TransactionHash);
    }

    private enum MintingState
    {
        CheckingNetwork,
        CheckingAddress,
        CheckingBalance,
        RequestingAirdrop,
        SendingTransaction,
        Done,
    }

    private async Task RequestAirdrop()
    {
        State = MintingState.RequestingAirdrop;
        ProgressBarText = "Requesting airdrop...";
        RefreshMediator.NotifyStateHasChangedSafe();

        var result = await AppState.SelectedNetwork.Airdrop(AppState.UserStorage.DestinationAddress);
        if (result.IsSuccess)
        {
            NotificationService.Notify(NotificationSeverity.Success, "Airdrop succeeded");
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, "Airdrop failed", result.Error);
        }

        ProgressBarText = null;
        RefreshMediator.NotifyStateHasChangedSafe();
        CheckNetwork();
    }
}
