namespace NftFaucet.Plugins.Models.Abstraction;

public interface INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; }
}
