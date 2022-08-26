namespace NftFaucetRadzen.Models;

public class ScopedAppState
{
    public StateStorage Storage { get; private set; } = new();

    public void Reset()
    {
        Storage = new StateStorage();
    }
}
