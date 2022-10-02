using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Components.CardList;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;
using Radzen;

namespace NftFaucet.Pages;

public partial class ProvidersPage : BasicComponent
{
    [Inject]
    public IServiceProvider ServiceProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Providers = AppState.PluginStorage.Providers.Where(x => AppState.SelectedNetwork != null && x.IsNetworkSupported(AppState.SelectedNetwork)).ToArray();
        foreach (var provider in Providers)
        {
            if (!provider.IsInitialized)
            {
                await provider.InitializeAsync(ServiceProvider);
            }
        }
        RefreshCards();
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    private IProvider[] Providers { get; set; }
    private CardListItem[] ProviderCards { get; set; }

    private void RefreshCards()
    {
        ProviderCards = Providers.Select(MapCardListItem).ToArray();
    }

    private CardListItem MapCardListItem(IProvider provider)
    {
        var configurationItems = provider.GetConfigurationItems();
        return new CardListItem
        {
            Id = provider.Id,
            ImageLocation = provider.ImageName != null ? "./images/" + provider.ImageName : null,
            Header = provider.Name,
            IsDisabled = !provider.IsSupported,
            Properties = provider.GetProperties().Select(Map).ToArray(),
            SelectionIcon = provider.IsConfigured ? CardListItemSelectionIcon.Checkmark : CardListItemSelectionIcon.Warning,
            Badges = new[]
            {
                (Settings?.RecommendedProviders?.Contains(provider.Id) ?? false)
                    ? new CardListItemBadge {Style = BadgeStyle.Success, Text = "Recommended"}
                    : null,
                !provider.IsSupported
                    ? new CardListItemBadge {Style = BadgeStyle.Light, Text = "Not Supported"}
                    : null,
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
                            var result = await OpenConfigurationDialog(provider);
                            RefreshCards();
                            if (result.IsSuccess)
                            {
                                await StateRepository.SaveProviderState(provider);
                            }
                        }
                    }
                }
                : Array.Empty<CardListItemButton>(),
        };
    }

    private async Task OnProviderChange()
    {
        await SaveAppState();
    }

    private async Task<Result> OpenConfigurationDialog(IProvider provider)
    {
        var configurationItems = provider.GetConfigurationItems();
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
                { nameof(ConfigurationDialog.ConfigureAction), provider.Configure },
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
