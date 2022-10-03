using NftFaucet.Infrastructure.Extensions;
using NftFaucet.Infrastructure.Models.State;
using NftFaucet.Infrastructure.Repositories;

namespace NftFaucet.Services;

public class InitializationService : IInitializationService
{
    private readonly ScopedAppState _appState;
    private readonly PluginLoader _pluginLoader;
    private readonly IStateRepository _stateRepository;
    private readonly IServiceProvider _serviceProvider;

    public InitializationService(ScopedAppState appState, PluginLoader pluginLoader, IStateRepository stateRepository, IServiceProvider serviceProvider)
    {
        _appState = appState;
        _pluginLoader = pluginLoader;
        _stateRepository = stateRepository;
        _serviceProvider = serviceProvider;
    }

    public async Task Initialize()
    {
        LoadDataFromPlugins();
        await LoadDataFromIndexedDb();
        await InitializeWallets();
    }

    private async Task InitializeWallets()
    {
        var wallets = _appState.PluginStorage.Wallets.Where(x => _appState.SelectedNetwork != null && x.IsNetworkSupported(_appState.SelectedNetwork)).ToArray();
        foreach (var wallet in wallets)
        {
            await wallet.InitializeAsync(_serviceProvider);
        }
    }

    private void LoadDataFromPlugins()
    {
        var isFirstRun = _appState.PluginStorage.Networks == null &&
                         _appState.PluginStorage.Wallets == null &&
                         _appState.PluginStorage.Uploaders == null &&
                         _appState.PluginStorage.Contracts == null;

        _appState.PluginStorage.Networks ??= _pluginLoader.NetworkPlugins.SelectMany(x => x.Networks).Where(x => x != null).ToArray();
        _appState.PluginStorage.Wallets ??= _pluginLoader.WalletPlugins.SelectMany(x => x.Wallets).Where(x => x != null).ToArray();
        _appState.PluginStorage.Uploaders ??= _pluginLoader.UploaderPlugins.SelectMany(x => x.Uploaders).Where(x => x != null).ToArray();
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
        _appState.UserStorage.UploadLocations = (await _stateRepository.LoadUploadLocations()).ToList();
        var walletStates = await _stateRepository.LoadWalletStates();
        foreach (var walletState in walletStates)
        {
            var wallet = _appState.PluginStorage.Wallets.FirstOrDefault(x => x.Id == walletState.Id);
            if (wallet == null)
            {
                continue;
            }
            await wallet.SetState(walletState.State);
        }
        var uploaderStates = await _stateRepository.LoadUploaderStates();
        foreach (var uploaderState in uploaderStates)
        {
            var uploader = _appState.PluginStorage.Uploaders.FirstOrDefault(x => x.Id == uploaderState.Id);
            if (uploader == null)
            {
                continue;
            }
            await uploader.SetState(uploaderState.State);
        }
    }

    private void ValidatePluginsData()
    {
        var networkIds = _appState.PluginStorage.Networks.Select(x => x.Id).ToArray();
        var walletIds = _appState.PluginStorage.Wallets.Select(x => x.Id).ToArray();
        var uploaderIds = _appState.PluginStorage.Uploaders.Select(x => x.Id).ToArray();
        var contractIds = _appState.PluginStorage.Contracts.Select(x => x.Id).ToArray();

        var networkIdDuplicates = networkIds.Duplicates().ToArray();
        if (networkIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are networks with same ids: {string.Join(", ", networkIdDuplicates)}");
        }

        var walletIdDuplicates = walletIds.Duplicates().ToArray();
        if (walletIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are wallets with same ids: {string.Join(", ", walletIdDuplicates)}");
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

        var allIds = networkIds.Concat(walletIds).Concat(uploaderIds).Concat(contractIds).ToArray();
        var allIdDuplicates = allIds.Duplicates().ToArray();
        if (allIdDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are plugin data items (networks/wallets/uploaders/contracts) with same ids: {string.Join(", ", allIdDuplicates)}");
        }

        var networkShortNames = _appState.PluginStorage.Networks.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var networkShortNameDuplicates = networkShortNames.Duplicates().ToArray();
        if (networkShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are networks with same short name: {string.Join(", ", networkShortNameDuplicates)}");
        }

        var walletShortNames = _appState.PluginStorage.Wallets.Select(x => x.ShortName).Where(x => x != null).ToArray();
        var walletShortNameDuplicates = walletShortNames.Duplicates().ToArray();
        if (walletShortNameDuplicates.Any())
        {
            throw new ApplicationException($"[{nameof(ValidatePluginsData)}] There are wallets with same short name: {string.Join(", ", walletShortNameDuplicates)}");
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
