using NftFaucetRadzen.Models.Dto;
using NftFaucetRadzen.Models.State;
using TG.Blazor.IndexedDB;

namespace NftFaucetRadzen.Services;

public class StateRepository
{
    private readonly IndexedDBManager _dbManager;
    private readonly Mapper _mapper;
    private const string AppStateStoreName = "AppState";

    public StateRepository(IndexedDBManager dbManager, Mapper mapper)
    {
        _dbManager = dbManager;
        _mapper = mapper;
    }

    public async Task SaveAppState(ScopedAppState appState)
    {
        var appStateDto = _mapper.ToDto(appState) ?? new AppStateDto();
        var record = new StoreRecord<AppStateDto>
        {
            Storename = AppStateStoreName,
            Data = appStateDto,
        };

        var existingAppStateDto = await GetFirst<AppStateDto>(AppStateStoreName);
        if (existingAppStateDto == null)
        {
            await _dbManager.AddRecord(record);
        }
        else
        {
            await _dbManager.UpdateRecord(record);
        }
    }

    public async Task LoadAppState(ScopedAppState appState)
    {
        var appStateDto = await GetFirst<AppStateDto>(AppStateStoreName) ?? new AppStateDto();
        var loadedAppState = _mapper.ToDomain(appStateDto);
        appState.LoadUserStorage(loadedAppState.UserStorage);
    }

    private async Task<T> GetFirst<T>(string storeName)
    {
        var existingRecords = await _dbManager.GetRecords<T>(storeName);
        if (existingRecords == null || existingRecords.Count == 0)
            return default;

        return existingRecords.FirstOrDefault();
    }
}
