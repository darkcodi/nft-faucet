using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins;

public interface IWalletPlugin
{
    public IReadOnlyCollection<IWallet> Wallets { get; }
}
