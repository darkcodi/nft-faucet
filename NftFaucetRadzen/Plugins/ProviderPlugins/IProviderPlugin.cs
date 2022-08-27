namespace NftFaucetRadzen.Plugins.ProviderPlugins;

public interface IProviderPlugin
{
    public IReadOnlyCollection<IProvider> Providers { get; }
}
