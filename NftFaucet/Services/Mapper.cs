using NftFaucet.Models.Dto;
using NftFaucet.Models.State;
using NftFaucet.Plugins;

namespace NftFaucet.Services;

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
            MainFileName = token.MainFile?.FileName,
            MainFileType = token.MainFile?.FileType,
            MainFileData = token.MainFile?.FileData,
            MainFileSize = token.MainFile?.FileSize,
            CoverFileName = token.CoverFile?.FileName,
            CoverFileType = token.CoverFile?.FileType,
            CoverFileData = token.CoverFile?.FileData,
            CoverFileSize = token.CoverFile?.FileSize,
        };

    public UploadLocationDto ToDto(ITokenUploadLocation uploadLocation)
        => uploadLocation == null ? null : new UploadLocationDto
        {
            Id = uploadLocation.Id,
            TokenId = uploadLocation.TokenId,
            Name = uploadLocation.Name,
            Location = uploadLocation.Location,
            CreatedAt = uploadLocation.CreatedAt,
            UploaderId = uploadLocation.UploaderId,
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
        => tokenDto == null || string.IsNullOrEmpty(tokenDto.MainFileData) ? null : new Token
        {
            Id = tokenDto.Id,
            Name = tokenDto.Name,
            Description = tokenDto.Description,
            CreatedAt = tokenDto.CreatedAt,
            MainFile = new TokenMedia
            {
                FileName = tokenDto.MainFileName,
                FileType = tokenDto.MainFileType,
                FileData = tokenDto.MainFileData,
                FileSize = tokenDto.MainFileSize ?? 0,
            },
            CoverFile = string.IsNullOrEmpty(tokenDto.CoverFileData) ? null : new TokenMedia
            {
                FileName = tokenDto.CoverFileName,
                FileType = tokenDto.CoverFileType,
                FileData = tokenDto.CoverFileData,
                FileSize = tokenDto.CoverFileSize ?? 0,
            },
        };

    public ITokenUploadLocation ToDomain(UploadLocationDto uploadLocationDto)
        => uploadLocationDto == null ? null : new TokenUploadLocation
        {
            Id = uploadLocationDto.Id,
            TokenId = uploadLocationDto.TokenId,
            Name = uploadLocationDto.Name,
            Location = uploadLocationDto.Location,
            CreatedAt = uploadLocationDto.CreatedAt,
            UploaderId = uploadLocationDto.UploaderId,
        };

    private Guid[] ToGuidArray(Guid? guid) => guid == null ? Array.Empty<Guid>() : new[] {guid.Value};
}
