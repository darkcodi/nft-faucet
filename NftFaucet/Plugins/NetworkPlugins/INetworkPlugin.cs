namespace NftFaucet.Plugins.NetworkPlugins;

public interface INetworkPlugin
{
    public IReadOnlyCollection<INetwork> Networks { get; }
}
