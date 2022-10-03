using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Infrastructure.Models.State;

public class PluginStateStorage
{
    public ICollection<INetwork> Networks { get; set; }
    public ICollection<IWallet> Wallets { get; set; }
    public ICollection<IUploader> Uploaders { get; set; }
    public ICollection<IContract> Contracts { get; set; }
}
