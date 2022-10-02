using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using NftFaucet.Domain.Attributes;

namespace NftFaucet.Domain.Function;

[Function("mint"), FunctionHash("0xd3fc9864")]
public class Erc1155MintFunction : Function
{
    [Parameter("address", "to", 1)]
    public string To { get; set; }

    [Parameter("uint256", "amount", 2)]
    public BigInteger Amount { get; set; }

    [Parameter("string", "tokenUri", 3)]
    public string Uri { get; set; }
}
