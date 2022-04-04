namespace NftFaucet.Models;

public class ScopedAppState
{
    public ScopedAppState(IpfsBlockchainContext context, MetamaskInfo metamask, NavigationWrapper navigationWrapper)
    {
        IpfsContext = context;
        Metamask = metamask;
        Navigation = navigationWrapper;
    }

    public IpfsBlockchainContext IpfsContext { get; }
    public MetamaskInfo Metamask { get; }
    public NavigationWrapper Navigation { get; }
    public StateStorage Storage { get; private set; } = new();

    public void Reset()
    {
        Storage = new StateStorage();
    }
}
