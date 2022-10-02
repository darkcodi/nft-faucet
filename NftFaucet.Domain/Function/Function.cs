using Nethereum.ABI.FunctionEncoding;
using Nethereum.Hex.HexConvertors.Extensions;
using NftFaucet.Domain.Attributes;
using NftFaucet.Domain.Extensions;

namespace NftFaucet.Domain.Function;

public abstract class Function
{
    public string Encode()
    {
        var hash = GetHash().EnsureHexPrefix();
        var encoder = new FunctionCallEncoder();
        var encodedParameters = encoder.EncodeParametersFromTypeAttributes(GetType(), this);
        var encodedCall = encoder.EncodeRequest(hash, encodedParameters.ToHex());
        return encodedCall;
    }

    public string GetHash() => GetType().GetAttribute<FunctionHashAttribute>().Hash;
}
