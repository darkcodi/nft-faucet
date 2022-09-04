using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins;

public interface IProvider
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }

    public bool CanBeConfigured { get; }
    public bool IsConfigured { get; }
    public void Configure();

    public List<(string Name, string Value)> GetProperties();
    public bool IsNetworkSupported(INetwork network);
}
