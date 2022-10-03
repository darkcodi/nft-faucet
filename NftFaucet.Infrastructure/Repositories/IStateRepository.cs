using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Infrastructure.Models.Dto;
using NftFaucet.Infrastructure.Models.State;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Infrastructure.Repositories;

public interface IStateRepository
{
    public Task SaveAppState(ScopedAppState appState);
    public Task SaveToken(IToken token);
    public Task SaveUploadLocation(ITokenUploadLocation uploadLocation);
    public Task SaveWalletState(IWallet wallet);
    public Task SaveUploaderState(IUploader uploader);

    public Task LoadAppState(ScopedAppState appState);
    public Task<IToken[]> LoadTokens();
    public Task<ITokenUploadLocation[]> LoadUploadLocations();
    public Task<UploaderStateDto[]> LoadUploaderStates();
    public Task<WalletStateDto[]> LoadWalletStates();
}
