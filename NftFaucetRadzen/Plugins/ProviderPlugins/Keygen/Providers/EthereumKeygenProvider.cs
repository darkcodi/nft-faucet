using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Plugins.ProviderPlugins.Keygen.Providers;

public class EthereumKeygenProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("ded55b2b-8139-4251-a0fc-ba620f9727c9");
    public string Name { get; } = "Ethereum keygen";
    public string ShortName { get; } = "EthKeygen";
    public string ImageName { get; } = "ecdsa.svg";
    public bool IsSupported { get; } = true;

    public bool IsInitialized { get; private set; }
    public EthereumKey Key { get; private set; }

    public void Initialize()
    {
        Key = EthereumKey.GenerateNew();
        IsInitialized = true;
    }

    public List<(string Name, string Value)> GetProperties()
        => new List<(string Name, string Value)>
        {
            ("Private key", Key?.PrivateKey ?? "<null>"),
            ("Address", Key?.Address ?? "<null>"),
        };
}
