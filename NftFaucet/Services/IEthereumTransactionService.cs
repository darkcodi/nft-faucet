using System.Numerics;
using NftFaucet.Models.Enums;

namespace NftFaucet.Services;

public interface IEthereumTransactionService
{
    Task<string> MintErc721Token(NetworkChain network, string destinationAddress, string tokenUri);
    Task<string> MintErc1155Token(NetworkChain network, string destinationAddress, BigInteger amount, string tokenUri);
}
