using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.WalletPlugins.Metamask;

public class MetamaskWalletPlugin : IWalletPlugin
{
    public IReadOnlyCollection<IWallet> Wallets { get; } = new IWallet[]
    {
        new MetamaskWallet(),
    };
}
