using NftFaucet.Plugins;
using NftFaucet.Plugins.NetworkPlugins;
using NftFaucet.Plugins.ProviderPlugins;

namespace NftFaucet.Models;

public record MintRequest(
    INetwork Network,
    IProvider Provider,
    IContract Contract,
    IToken Token,
    ITokenUploadLocation UploadLocation,
    string DestinationAddress,
    int TokensAmount);
