using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.WalletPlugins.Phantom;

public class PhantomWalletPlugin : IWalletPlugin
{
    public IReadOnlyCollection<IWallet> Wallets { get; } = new IWallet[]
    {
        new PhantomWallet(),
    };
}
