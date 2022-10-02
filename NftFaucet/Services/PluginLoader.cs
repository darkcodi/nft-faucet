using NftFaucet.NetworkPlugins.Arbitrum;
using NftFaucet.NetworkPlugins.Avalanche;
using NftFaucet.NetworkPlugins.BinanceSmartChain;
using NftFaucet.NetworkPlugins.Ethereum;
using NftFaucet.NetworkPlugins.Moonbeam;
using NftFaucet.NetworkPlugins.Optimism;
using NftFaucet.NetworkPlugins.Polygon;
using NftFaucet.NetworkPlugins.Solana;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.ProviderPlugins.EthereumKeygen;
using NftFaucet.ProviderPlugins.Metamask;
using NftFaucet.ProviderPlugins.Phantom;
using NftFaucet.ProviderPlugins.SolanaKeygen;
using NftFaucet.UploadPlugins.Crust;
using NftFaucet.UploadPlugins.Infura;
using NftFaucet.UploadPlugins.NftStorage;

namespace NftFaucet.Services;

public class PluginLoader
{
    public IReadOnlyCollection<INetworkPlugin> NetworkPlugins { get; } = new INetworkPlugin[]
    {
        new EthereumNetworkPlugin(),
        new PolygonNetworkPlugin(),
        new BscNetworkPlugin(),
        new OptimismNetworkPlugin(),
        new MoonbeamNetworkPlugin(),
        new ArbitrumNetworkPlugin(),
        new AvalancheNetworkPlugin(),
        new SolanaNetworkPlugin(),
    };

    public IReadOnlyCollection<IProvider> ProviderPlugins { get; } = new IProvider[]
    {
        new MetamaskProvider(),
        new EthereumKeygenProvider(),
        new PhantomProvider(),
        new SolanaKeygenProvider(),
    };

    public IReadOnlyCollection<IUploader> UploadPlugins { get; } = new IUploader[]
    {
        new InfuraUploader(),
        new NftStorageUploader(),
        new CrustUploader(),
    };
}
