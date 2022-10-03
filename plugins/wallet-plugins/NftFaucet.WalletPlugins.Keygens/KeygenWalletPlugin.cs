using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.WalletPlugins.Keygens;

public class KeygenWalletPlugin : IWalletPlugin
{
    public IReadOnlyCollection<IWallet> Wallets { get; } = new IWallet[]
    {
        new EthereumKeygenWallet(),
        new SolanaKeygenWallet(),
    };
}
