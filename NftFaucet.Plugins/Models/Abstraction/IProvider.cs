using NftFaucet.Domain.Models;

namespace NftFaucet.Plugins.Models.Abstraction;

public interface IProvider : INamedEntity, IStateful, IInitializable, IEntityWithProperties, IConfigurable
{
    public bool IsNetworkSupported(INetwork network);
    public Task<string> GetAddress();
    public Task<Balance> GetBalance(INetwork network);
    public Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork);
    public Task<string> Mint(MintRequest mintRequest);
}
