using NftFaucetRadzen.Plugins.UploadPlugins.InfuraDedicatedGateway.Uploaders;

namespace NftFaucetRadzen.Plugins.UploadPlugins.InfuraDedicatedGateway;

public class InfuraDedicatedGatewayUploadPlugin : IUploadPlugin
{
    public IReadOnlyCollection<IUploader> Uploaders { get; } = new[]
    {
        new InfuraDedicatedGatewayUploader(),
    };
}
