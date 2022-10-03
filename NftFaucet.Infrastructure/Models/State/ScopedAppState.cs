using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Infrastructure.Models.State;

public class ScopedAppState
{
    public PluginStateStorage PluginStorage { get; private set; } = new();
    public UserStateStorage UserStorage { get; private set; } = new();

    public INetwork SelectedNetwork => PluginStorage?.Networks?.FirstOrDefault(x => x.Id == UserStorage?.SelectedNetworks?.FirstOrDefault());
    public IWallet SelectedWallet => PluginStorage?.Wallets?.FirstOrDefault(x => x.Id == UserStorage?.SelectedWallets?.FirstOrDefault());
    public IContract SelectedContract => PluginStorage?.Contracts?.FirstOrDefault(x => x.Id == UserStorage?.SelectedContracts?.FirstOrDefault());
    public IToken SelectedToken => UserStorage?.Tokens?.FirstOrDefault(x => x.Id == UserStorage?.SelectedTokens?.FirstOrDefault());
    public ITokenUploadLocation SelectedUploadLocation => UserStorage?.UploadLocations?.FirstOrDefault(x => x.Id == UserStorage?.SelectedUploadLocations?.FirstOrDefault());

    public void LoadUserStorage(UserStateStorage userStorage)
    {
        UserStorage = userStorage ?? new();
    }
}
