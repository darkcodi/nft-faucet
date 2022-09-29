using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.Dto;
using NftFaucetRadzen.Models.State;

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

    private Guid[] ToGuidArray(Guid? guid) => guid == null ? Array.Empty<Guid>() : new[] {guid.Value};
}
