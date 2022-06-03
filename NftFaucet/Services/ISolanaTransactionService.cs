using NftFaucet.Models.Enums;

namespace NftFaucet.Services;

public interface ISolanaTransactionService
{
    Task<string> MintNft(EthereumNetwork chain, string destinationAddress, string tokenUri, string name, double amount);
}
