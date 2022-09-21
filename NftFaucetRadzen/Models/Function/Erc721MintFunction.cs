using Nethereum.ABI.FunctionEncoding.Attributes;
using NftFaucetRadzen.Attributes;

namespace NftFaucetRadzen.Models.Function;

[Function("safeMint"), FunctionHash("0xd204c45e")]
public class Erc721MintFunction : Function
{
    [Parameter("address", "to", 1)]
    public string To { get; set; }

    [Parameter("string", "uri", 2)]
    public string Uri { get; set; }
}
