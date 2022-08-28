using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Models;

public class StateStorage
{
    public IReadOnlyCollection<INetwork> Networks { get; set; }
    public IReadOnlyCollection<IProvider> Providers { get; set; }
    public IReadOnlyCollection<IContract> Contracts { get; set; }

    public Guid[] SelectedNetworks { get; set; }
    public Guid[] SelectedProviders { get; set; }
    public Guid[] SelectedContracts { get; set; }
}
