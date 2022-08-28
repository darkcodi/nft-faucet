using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Models;

public class StateStorage
{
    public ICollection<INetwork> Networks { get; set; }
    public ICollection<IProvider> Providers { get; set; }
    public ICollection<IContract> Contracts { get; set; }
    public ICollection<IToken> Tokens { get; set; }
    public ICollection<ITokenUploadLocation> UploadLocations { get; set; }

    public Guid[] SelectedNetworks { get; set; }
    public Guid[] SelectedProviders { get; set; }
    public Guid[] SelectedContracts { get; set; }
    public Guid[] SelectedTokens { get; set; }
    public Guid[] SelectedUploadLocations { get; set; }
}
