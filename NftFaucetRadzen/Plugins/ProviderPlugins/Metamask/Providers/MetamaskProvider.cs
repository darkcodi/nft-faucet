namespace NftFaucetRadzen.Plugins.ProviderPlugins.Metamask.Providers;

public class MetamaskProvider : IProvider
{
    public Guid Id { get; } = Guid.Parse("3367b9bb-f50c-4768-aeb3-9ac14d4c3987");
    public string Name { get; } = "Metamask";
    public string ShortName { get; } = "Metamask";
    public string ImageName { get; } = "metamask_fox.svg";
    public bool IsSupported { get; } = true;

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }
}
