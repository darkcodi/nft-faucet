using CSharpFunctionalExtensions;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;

namespace NftFaucet.Models;

public class SolanaAddress : ValueObject<SolanaAddress>
{
    private SolanaAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(SolanaAddress address) => address.Value;
    public static explicit operator SolanaAddress(string address) => Create(address).Value;

    public static Result<SolanaAddress> Create(string address)
    {
        return new SolanaAddress(address);
    }

    public override string ToString() => Value;
    public string ToShortFormatString() => ToString();
    public string ToLongFormatString() => Value.RemoveHexPrefix();

    protected override bool EqualsCore(SolanaAddress other)
        => string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);

    protected override int GetHashCodeCore() => Value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
}
