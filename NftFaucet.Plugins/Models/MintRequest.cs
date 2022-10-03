using NftFaucet.Domain.Models.Abstraction;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.Plugins.Models;

public record MintRequest(
    INetwork Network,
    IWallet Wallet,
    IContract Contract,
    IToken Token,
    ITokenUploadLocation UploadLocation,
    string DestinationAddress,
    int TokensAmount);
