namespace NftFaucetRadzen.Plugins.UploadPlugins.InfuraDedicatedGateway.Uploaders;

public class InfuraDedicatedGatewayUploader : IUploader
{
    public Guid Id { get; } = Guid.Parse("c0d79c82-8e35-4cd6-ad35-bbe378088308");
    public string Name { get; } = "Infura dedicated IPFS gateway";
    public string ShortName { get; } = "InfuraDED";
    public string ImageName { get; } = "infura_black.svg";
    public bool IsSupported { get; } = true;
    public bool IsInitialized { get; private set; } = false;
}
