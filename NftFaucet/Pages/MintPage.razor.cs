using CSharpFunctionalExtensions;
using NftFaucet.Components;
using NftFaucet.Domain.Utils;
using Radzen;

namespace NftFaucet.Pages;

public partial class MintPage : BasicComponent
{
    private string SourceAddress { get; set; }
    private bool IsReadyToMint => AppState != null &&
                                  AppState.SelectedNetwork != null &&
                                  AppState.SelectedProvider != null &&
                                  AppState.SelectedProvider.IsConfigured &&
                                  AppState.SelectedContract != null &&
                                  AppState.SelectedToken != null &&
                                  AppState.SelectedUploadLocation != null &&
                                  AppState.UserStorage.DestinationAddress != null;

    protected override async Task OnInitializedAsync()
    {
        if (AppState?.SelectedProvider?.IsConfigured ?? false)
        {
            SourceAddress = await ResultWrapper.Wrap(() => AppState.SelectedProvider.GetAddress()).Match(x => x, _ => null);
            AppState.UserStorage.DestinationAddress = SourceAddress;
        }
    }

    private async Task Mint()
    {
        await DialogService.OpenAsync<MintDialog>("Minting...",
            new Dictionary<string, object>(),
            new DialogOptions() { Width = "700px", Height = "570px", Resizable = true, Draggable = true });
    }
}
