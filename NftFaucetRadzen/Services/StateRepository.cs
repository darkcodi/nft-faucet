using NftFaucetRadzen.Models.Dto;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Plugins;
using TG.Blazor.IndexedDB;

namespace NftFaucetRadzen.Services;

public class StateRepository
{
    private readonly IndexedDBManager _dbManager;
    private readonly Mapper _mapper;
    private const string AppStateStoreName = "AppState";
    private const string TokensStoreName = "Tokens";

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

    public async Task SaveToken(IToken token)
    {
        var tokenDto = _mapper.ToDto(token) ?? new TokenDto();
        var record = new StoreRecord<TokenDto>
        {
            Storename = TokensStoreName,
            Data = tokenDto,
        };

        var existingTokenDto = await _dbManager.GetRecordById<Guid, TokenDto>(TokensStoreName, tokenDto.Id);
        if (existingTokenDto == null)
        {
            await _dbManager.AddRecord(record);
        }
        else
        {
            await _dbManager.UpdateRecord(record);
        }
    }

    public async Task<IToken[]> LoadTokens()
    {
        var existingTokens = await _dbManager.GetRecords<TokenDto>(TokensStoreName);
        if (existingTokens == null || existingTokens.Count == 0)
            return Array.Empty<IToken>();

        return existingTokens.Select(_mapper.ToDomain).ToArray();
    }

    private async Task<T> GetFirst<T>(string storeName)
    {
        var existingRecords = await _dbManager.GetRecords<T>(storeName);
        if (existingRecords == null || existingRecords.Count == 0)
            return default;

        return existingRecords.FirstOrDefault();
    }
}
