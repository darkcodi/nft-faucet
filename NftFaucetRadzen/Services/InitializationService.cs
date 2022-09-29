using NftFaucetRadzen.Extensions;
using NftFaucetRadzen.Models.State;

namespace NftFaucetRadzen.Services;

public class InitializationService
{
    private readonly ScopedAppState _appState;
    private readonly PluginLoader _pluginLoader;
    private readonly StateRepository _stateRepository;

    public InitializationService(ScopedAppState appState, PluginLoader pluginLoader, StateRepository stateRepository)
    {
        _appState = appState;
        _pluginLoader = pluginLoader;
        _stateRepository = stateRepository;
    }

    public async Task Initialize()
    {
        LoadDataFromPlugins();
        await LoadDataFromIndexedDb();
    }

    private void LoadDataFromPlugins()
    {
        var isFirstRun = _appState.PluginStorage.Networks == null &&
                         _appState.PluginStorage.Providers == null &&
                         _appState.PluginStorage.Uploaders == null &&
                         _appState.PluginStorage.Contracts == null;

        _pluginLoader.EnsurePluginsLoaded();
        _appState.PluginStorage.Networks ??= _pluginLoader.NetworkPlugins.SelectMany(x => x.Networks).Where(x => x != null).ToArray();
        _appState.PluginStorage.Providers ??= _pluginLoader.ProviderPlugins.SelectMany(x => x.Providers).Where(x => x != null).ToArray();
        _appState.PluginStorage.Uploaders ??= _pluginLoader.UploadPlugins.SelectMany(x => x.Uploaders).Where(x => x != null).ToArray();
        _appState.PluginStorage.Contracts ??= _appState.PluginStorage.Networks.SelectMany(x => x.DeployedContracts).Where(x => x != null).ToArray();

        if (isFirstRun)
        {
            ValidatePluginsData();
        }
    }

    private async Task LoadDataFromIndexedDb()
    {
        await _stateRepository.LoadAppState(_appState);
        _appState.UserStorage.Tokens = (await _stateRepository.LoadTokens()).ToList();
    }

    private void ValidatePluginsData()
    {
        var networkIds = _appState.PluginStorage.Networks.Select(x => x.Id).ToArray();
        var providerIds = _appState.PluginStorage.Providers.Select(x => x.Id).ToArray();
        var uploaderIds = _appState.PluginStorage.Uploaders.Select(x => x.Id).ToArray();
        var contractIds = _appState.PluginStorage.Contracts.Select(x => x.Id).ToArray();

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

        var networkShortNames = _appState.PluginStorage.Networks.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var networkShortNameDuplicates = networkShortNames.Duplicates().ToArray();
        if (networkShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are networks with same short name: {string.Join(", ", networkShortNameDuplicates)}");
        }

        var providerShortNames = _appState.PluginStorage.Providers.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var providerShortNameDuplicates = providerShortNames.Duplicates().ToArray();
        if (providerShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are providers with same short name: {string.Join(", ", providerShortNameDuplicates)}");
        }

        var uploaderShortNames = _appState.PluginStorage.Uploaders.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var uploaderShortNameDuplicates = uploaderShortNames.Duplicates().ToArray();
        if (uploaderShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are uploaders with same short name: {string.Join(", ", uploaderShortNameDuplicates)}");
        }

        var txHashes = _appState.PluginStorage.Contracts.Select(x => x.DeploymentTxHash).Where(x => x != null).ToArray();
        var txHashDuplicates = txHashes.Duplicates().ToArray();
        if (txHashDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are contracts with same tx hash: {string.Join(", ", txHashDuplicates)}");
        }

        var txDeploymentDates = _appState.PluginStorage.Contracts.Select(x => x.DeployedAt).Where(x => x != null).ToArray();
        var txDeploymentDateDuplicates = txDeploymentDates.Duplicates().ToArray();
        if (txDeploymentDateDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are contracts with same deployment datetime: {string.Join(", ", txDeploymentDateDuplicates)}");
        }
    }
}
