using NftFaucet.NetworkPlugins.Arbitrum;
using NftFaucet.NetworkPlugins.Avalanche;
using NftFaucet.NetworkPlugins.BinanceSmartChain;
using NftFaucet.NetworkPlugins.Ethereum;
using NftFaucet.NetworkPlugins.Moonbeam;
using NftFaucet.NetworkPlugins.Optimism;
using NftFaucet.NetworkPlugins.Polygon;
using NftFaucet.NetworkPlugins.Solana;
using NftFaucet.Plugins;
using NftFaucet.Plugins.Models.Abstraction;
using NftFaucet.ProviderPlugins.EthereumKeygen;
using NftFaucet.ProviderPlugins.Keygens;
using NftFaucet.ProviderPlugins.Metamask;
using NftFaucet.ProviderPlugins.Phantom;
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

    public IReadOnlyCollection<IProviderPlugin> ProviderPlugins { get; } = new IProviderPlugin[]
    {
        new MetamaskProviderPlugin(),
        new PhantomProviderPlugin(),
        new KeygenProviderPlugin(),
    };

    public IReadOnlyCollection<IUploaderPlugin> UploaderPlugins { get; } = new IUploaderPlugin[]
    {
        new InfuraUploaderPlugin(),
        new NftStorageUploaderPlugin(),
        new CrustUploaderPlugin(),
    };
}
