using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Extensions;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Services;

namespace NftFaucetRadzen.Components;

public abstract class BasicLayout : LayoutComponentBase
{
    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected ScopedAppState AppState { get; set; }

    [Inject]
    protected RefreshMediator RefreshMediator { get; set; }

    [Inject]
    protected PluginLoader PluginLoader { get; set; }

    [Inject]
    protected StateRepository StateRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LoadDataFromPlugins();
        await LoadDataFromIndexedDb();
        RefreshMediator.StateChanged += async () => await InvokeAsync(StateHasChangedSafe);
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    protected void StateHasChangedSafe()
    {
        try
        {
            InvokeAsync(StateHasChanged);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private void LoadDataFromPlugins()
    {
        var isFirstRun = AppState.PluginStorage.Networks == null &&
                         AppState.PluginStorage.Providers == null &&
                         AppState.PluginStorage.Uploaders == null &&
                         AppState.PluginStorage.Contracts == null;

        PluginLoader.EnsurePluginsLoaded();
        AppState.PluginStorage.Networks ??= PluginLoader.NetworkPlugins.SelectMany(x => x.Networks).Where(x => x != null).ToArray();
        AppState.PluginStorage.Providers ??= PluginLoader.ProviderPlugins.SelectMany(x => x.Providers).Where(x => x != null).ToArray();
        AppState.PluginStorage.Uploaders ??= PluginLoader.UploadPlugins.SelectMany(x => x.Uploaders).Where(x => x != null).ToArray();
        AppState.PluginStorage.Contracts ??= AppState.PluginStorage.Networks.SelectMany(x => x.DeployedContracts).Where(x => x != null).ToArray();

        if (isFirstRun)
        {
            ValidatePluginsData();
        }
    }

    private async Task LoadDataFromIndexedDb()
    {
        if (AppState.IsUserStorageLoaded)
            return;

        await StateRepository.LoadAppState(AppState);
    }

    private void ValidatePluginsData()
    {
        var networkIds = AppState.PluginStorage.Networks.Select(x => x.Id).ToArray();
        var providerIds = AppState.PluginStorage.Providers.Select(x => x.Id).ToArray();
        var uploaderIds = AppState.PluginStorage.Uploaders.Select(x => x.Id).ToArray();
        var contractIds = AppState.PluginStorage.Contracts.Select(x => x.Id).ToArray();

        var networkIdDuplicates = networkIds.Duplicates().ToArray();
        if (networkIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are networks with same ids: {string.Join(", ", networkIdDuplicates)}");
        }

        var providerIdDuplicates = providerIds.Duplicates().ToArray();
        if (providerIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are providers with same ids: {string.Join(", ", providerIdDuplicates)}");
        }

        var uploaderIdDuplicates = uploaderIds.Duplicates().ToArray();
        if (uploaderIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are uploaders with same ids: {string.Join(", ", uploaderIdDuplicates)}");
        }

        var contractIdDuplicates = contractIds.Duplicates().ToArray();
        if (contractIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are contracts with same ids: {string.Join(", ", contractIdDuplicates)}");
        }

        var allIds = networkIds.Concat(providerIds).Concat(uploaderIds).Concat(contractIds).ToArray();
        var allIdDuplicates = allIds.Duplicates().ToArray();
        if (allIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are plugin data items (networks/providers/uploaders/contracts) with same ids: {string.Join(", ", allIdDuplicates)}");
        }

        var networkShortNames = AppState.PluginStorage.Networks.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var networkShortNameDuplicates = networkShortNames.Duplicates().ToArray();
        if (networkShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are networks with same short name: {string.Join(", ", networkShortNameDuplicates)}");
        }

        var providerShortNames = AppState.PluginStorage.Providers.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var providerShortNameDuplicates = providerShortNames.Duplicates().ToArray();
        if (providerShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are providers with same short name: {string.Join(", ", providerShortNameDuplicates)}");
        }

        var uploaderShortNames = AppState.PluginStorage.Uploaders.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var uploaderShortNameDuplicates = uploaderShortNames.Duplicates().ToArray();
        if (uploaderShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are uploaders with same short name: {string.Join(", ", uploaderShortNameDuplicates)}");
        }

        var txHashes = AppState.PluginStorage.Contracts.Select(x => x.DeploymentTxHash).Where(x => x != null).ToArray();
        var txHashDuplicates = txHashes.Duplicates().ToArray();
        if (txHashDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are contracts with same tx hash: {string.Join(", ", txHashDuplicates)}");
        }

        var txDeploymentDates = AppState.PluginStorage.Contracts.Select(x => x.DeployedAt).Where(x => x != null).ToArray();
        var txDeploymentDateDuplicates = txDeploymentDates.Duplicates().ToArray();
        if (txDeploymentDateDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are contracts with same deployment datetime: {string.Join(", ", txDeploymentDateDuplicates)}");
        }
    }
}
