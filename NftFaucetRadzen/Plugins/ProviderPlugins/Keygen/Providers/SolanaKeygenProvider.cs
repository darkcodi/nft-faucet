using NftFaucetRadzen.Plugins.NetworkPlugins;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Keygen.Providers;

public class SolanaKeygenProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("4c1a8ac5-60ca-4024-aae6-3c9852a6535c");
    public string Name { get; } = "Solana keygen";
    public string ShortName { get; } = "SolKeygen";
    public string ImageName { get; } = "ecdsa.svg";
    public bool IsSupported { get; } = false;

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }

    public List<(string Name, string Value)> GetProperties()
        => new List<(string Name, string Value)>
        {
            ("Private key", "<null>"),
            ("Address", "<null>"),
        };

    public bool IsNetworkSupported(INetwork network)
        => network.Type == NetworkType.Solana;
}
