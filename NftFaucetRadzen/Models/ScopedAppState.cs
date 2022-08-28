using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Models;

public class ScopedAppState
{
    public StateStorage Storage { get; private set; } = new();

    public INetwork SelectedNetwork => Storage?.Networks?.FirstOrDefault(x => x.Id == Storage?.SelectedNetworks?.FirstOrDefault());
    public IProvider SelectedProvider => Storage?.Providers?.FirstOrDefault(x => x.Id == Storage?.SelectedProviders?.FirstOrDefault());
    public IContract SelectedContract => Storage?.Contracts?.FirstOrDefault(x => x.Id == Storage?.SelectedContracts?.FirstOrDefault());
    public IToken SelectedToken => Storage?.Tokens?.FirstOrDefault(x => x.Id == Storage?.SelectedTokens?.FirstOrDefault());

    public void Reset()
    {
        Storage = new StateStorage();
    }
}
