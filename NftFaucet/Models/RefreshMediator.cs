namespace NftFaucet.Models;

public class RefreshMediator
{
    public delegate void StateChangedDelegate();
    public event StateChangedDelegate StateChanged;

    public void NotifyStateHasChangedSafe()
    {
        try
        {
            StateChanged?.Invoke();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
