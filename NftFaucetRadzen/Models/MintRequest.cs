using NftFaucetRadzen.Plugins;
using NftFaucetRadzen.Plugins.NetworkPlugins;
using NftFaucetRadzen.Plugins.ProviderPlugins;

namespace NftFaucetRadzen.Models;

public record MintRequest(
    INetwork Network,
    IProvider Provider,
    IContract Contract,
    IToken Token,
    ITokenUploadLocation UploadLocation,
    string DestinationAddress,
    int TokensAmount);
