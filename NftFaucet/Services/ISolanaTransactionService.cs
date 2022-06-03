using NftFaucet.Models.Enums;

namespace NftFaucet.Services;

public interface ISolanaTransactionService
{
    Task<string> MintNft(EthereumNetwork chain,
        string destinationAddress,
        string tokenUri,
        string name,
        string symbol,
        bool isTokenMutable,
        uint sellerFeeBasisPoints,
        ulong amount);
}
