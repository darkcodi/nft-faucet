using System.Numerics;
using NftFaucet.Models;
using NftFaucet.Models.Enums;
using NftFaucet.Models.Function;
using NftFaucet.Options;

namespace NftFaucet.Services;

public class EthereumTransactionService : IEthereumTransactionService
{
    private readonly Settings _settings;
    private readonly MetamaskInfo _metamaskInfo;

    public EthereumTransactionService(Settings settings, MetamaskInfo metamaskInfo)
    {
        _settings = settings;
        _metamaskInfo = metamaskInfo;
    }

    public async Task<string> MintErc721Token(EthereumNetwork network, string destinationAddress, string tokenUri)
    {
        var options = _settings.GetEthereumNetworkOptions(network);
        var transfer = new Erc721MintFunction
        {
            To = destinationAddress,
            Uri = tokenUri,
        };
        var data = transfer.Encode();
        var transactionHash = await _metamaskInfo.Service.SendTransaction(options.Erc721ContractAddress, 0, data);
        return transactionHash;
    }

    public async Task<string> MintErc1155Token(EthereumNetwork network, string destinationAddress, BigInteger amount, string tokenUri)
    {
        var options = _settings.GetEthereumNetworkOptions(network);
        var transfer = new Erc1155MintFunction
        {
            To = destinationAddress,
            Amount = amount,
            Uri = tokenUri,
        };
        var data = transfer.Encode();
        var transactionHash = await _metamaskInfo.Service.SendTransaction(options.Erc1155ContractAddress, 0, data);
        return transactionHash;
    }
}
