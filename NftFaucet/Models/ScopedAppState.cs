namespace NftFaucet.Models;

public class ScopedAppState
{
    public ScopedAppState(MetamaskInfo metamask, NavigationWrapper navigationWrapper)
    {
        Metamask = metamask;
        Navigation = navigationWrapper;
    }

    public MetamaskInfo Metamask { get; }
    public NavigationWrapper Navigation { get; }
    public StateStorage Storage { get; private set; } = new();

    public void Reset()
    {
        Storage = new StateStorage();
    }
}
