using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using Radzen;

#pragma warning disable CS8974

namespace NftFaucet.Pages;

public partial class WalletsPage : BasicComponent
{
    [Inject]
    public IServiceProvider ServiceProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Wallets = AppState.PluginStorage.Wallets.Where(x => AppState.SelectedNetwork != null && x.IsNetworkSupported(AppState.SelectedNetwork)).ToArray();
        foreach (var wallet in Wallets)
        {
            if (!wallet.IsInitialized)
            {
                await wallet.InitializeAsync(ServiceProvider);
            }
        }
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    private IWallet[] Wallets { get; set; }
    private CardListItem[] WalletCards { get; set; }

    private void RefreshCards()
    {
        WalletCards = Wallets.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IWallet model)
    {
        var configurationItems = model.GetConfigurationItems();
        return new CardListItem
        {
            Id = model.Id,
            ImageLocation = model.ImageName != null ? "./images/" + model.ImageName : null,
            Header = model.Name,
            IsDisabled = !model.IsSupported,
            Properties = model.GetProperties().Select(Map).ToArray(),
            SelectionIcon = model.IsConfigured ? CardListItemSelectionIcon.Checkmark : CardListItemSelectionIcon.Warning,
            Badges = new[]
            {
                (Settings?.RecommendedWallets?.Contains(model.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !model.IsSupported ? new CardListItemBadge { Style = BadgeStyle.Light, Text = "Not Supported" } : null,
                model.IsDeprecated ? new CardListItemBadge { Style = BadgeStyle.Warning, Text = "Deprecated" } : null,
            }.Where(x => x != null).ToArray(),
            Buttons = configurationItems != null && configurationItems.Any()
                ? new[]
                {
                    new CardListItemButton
                    {
                        Icon = "build",
                        Style = ButtonStyle.Secondary,
                        Action = async () =>
                        {
                            var result = await OpenConfigurationDialog(model);
                            RefreshCards();
                            if (result.IsSuccess)
                            {
                                await StateRepository.SaveWalletState(model);
                            }
                        }
                    }
                }
                : Array.Empty<CardListItemButton>(),
        };
    }

    private async Task OnWalletChange()
    {
        await SaveAppState();
    }

    private async Task<Result> OpenConfigurationDialog(IWallet wallet)
    {
        var configurationItems = wallet.GetConfigurationItems();
        foreach (var configurationItem in configurationItems)
        {
            var prevClickHandler = configurationItem.ClickAction;
            if (prevClickHandler != null)
            {
                configurationItem.ClickAction = () =>
                {
                    prevClickHandler();
                    RefreshMediator.NotifyStateHasChangedSafe();
                };
            }
        }

        var result = (bool?) await DialogService.OpenAsync<ConfigurationDialog>("Configuration",
            new Dictionary<string, object>
            {
                { nameof(ConfigurationDialog.ConfigurationItems), configurationItems },
                { nameof(ConfigurationDialog.ConfigureAction), wallet.Configure },
            },
            new DialogOptions() {Width = "700px", Height = "570px", Resizable = true, Draggable = true});

        return result != null && result.Value ? Result.Success() : Result.Failure("Operation cancelled");
    }

    private CardListItemProperty Map(Property model)
        => model == null ? null : new CardListItemProperty
        {
            Name = model.Name,
            Value = model.Value,
            ValueColor = model.ValueColor,
            Link = model.Link,
        };
}
