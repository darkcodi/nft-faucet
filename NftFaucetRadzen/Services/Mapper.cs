using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Dto;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Plugins;

namespace NftFaucetRadzen.Services;

public class Mapper
{
    public AppStateDto ToDto(ScopedAppState appState)
        => appState == null ? null : new AppStateDto
        {
            SelectedNetwork = appState.SelectedNetwork?.Id,
            SelectedProvider = appState.SelectedProvider?.Id,
            SelectedContract = appState.SelectedContract?.Id,
            SelectedToken = appState.SelectedToken?.Id,
            SelectedUploadLocation = appState.SelectedUploadLocation?.Id,
            DestinationAddress = appState.UserStorage?.DestinationAddress,
            TokenAmount = appState.UserStorage?.TokenAmount,
        };

    public TokenDto ToDto(IToken token)
        => token == null ? null : new TokenDto
        {
            Id = token.Id,
            Name = token.Name,
            Description = token.Description,
            CreatedAt = token.CreatedAt,
            ImageFileName = token.Image?.FileName,
            ImageFileType = token.Image?.FileType,
            ImageFileData = token.Image?.FileData,
            ImageFileSize = token.Image?.FileSize,
        };

    public ScopedAppState ToDomain(AppStateDto appStateDto)
        => appStateDto == null ? null : new ScopedAppState
        {
            UserStorage =
            {
                SelectedNetworks = ToGuidArray(appStateDto.SelectedNetwork),
                SelectedProviders = ToGuidArray(appStateDto.SelectedProvider),
                SelectedContracts = ToGuidArray(appStateDto.SelectedContract),
                SelectedTokens = ToGuidArray(appStateDto.SelectedToken),
                SelectedUploadLocations = ToGuidArray(appStateDto.SelectedUploadLocation),
                DestinationAddress = appStateDto.DestinationAddress,
                TokenAmount = appStateDto.TokenAmount ?? 1,
            }
        };

    public IToken ToDomain(TokenDto tokenDto)
        => tokenDto == null ? null : new Token
        {
            Id = tokenDto.Id,
            Name = tokenDto.Name,
            Description = tokenDto.Description,
            CreatedAt = tokenDto.CreatedAt,
            Image = new TokenMedia
            {
                FileName = tokenDto.ImageFileName,
                FileType = tokenDto.ImageFileType,
                FileData = tokenDto.ImageFileData,
                FileSize = tokenDto.ImageFileSize ?? 0,
            },
        };

    private Guid[] ToGuidArray(Guid? guid) => guid == null ? Array.Empty<Guid>() : new[] {guid.Value};
}
