using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins;

public interface IProvider
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
    public bool IsInitialized { get; }

    public void Initialize();
    public List<(string Name, string Value)> GetProperties();
    // public bool IsNetworkSupported(INetwork network);
}
