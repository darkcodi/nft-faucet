using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Utils;

namespace NftFaucetRadzen.Pages;

public partial class MintDialog : BasicComponent
{
    private const int MinDelayInMilliseconds = 300;

    private string ProgressBarText { get; set; } = "Checking network...";
    private MintingState State { get; set; } = MintingState.CheckingNetwork;
    private INetwork ProviderNetwork { get; set; }
    private string SourceAddress { get; set; }
    private Balance Balance { get; set; }
    private string TransactionHash { get; set; }
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

        var providerNetworkResult = await ResultWrapper.Wrap(async () =>
        {
            var task1 = AppState.SelectedProvider.GetNetwork(AppState.PluginStorage.Networks.ToArray(), AppState.SelectedNetwork);
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        ProviderNetwork = providerNetworkResult.IsSuccess ? providerNetworkResult.Value : null;

        if (ProviderNetwork?.Id == AppState.SelectedNetwork.Id)
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
            var task1 = AppState.SelectedProvider.GetAddress();
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
            var task1 = AppState.SelectedProvider.GetBalance(AppState.SelectedNetwork);
            var task2 = Task.Delay(TimeSpan.FromMilliseconds(MinDelayInMilliseconds));
            await Task.WhenAll(task1, task2);
            return task1.Result;
        });
        Balance = balanceResult.IsSuccess ? balanceResult.Value : null;
        var amount = Math.Max(Balance?.Amount ?? 0, 0);

        if (amount != 0)
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
            var mintRequest = new MintRequest(AppState.SelectedNetwork, AppState.SelectedProvider,
                AppState.SelectedContract, AppState.SelectedToken, AppState.SelectedUploadLocation,
                AppState.UserStorage.DestinationAddress, AppState.UserStorage.TokenAmount);
            var task1 = AppState.SelectedProvider.Mint(mintRequest);
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

    private async Task Close()
    {
        DialogService.Close(TransactionHash);
    }
}
