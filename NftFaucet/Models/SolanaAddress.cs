using System.Text.RegularExpressions;
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

    public static Result<SolanaAddress> Create(string value)
    {
        var regex = "^[1-9A-HJ-NP-Za-km-z]{32,44}$";

        if (!Regex.IsMatch(value, regex))
        {
            return Result.Failure<SolanaAddress>("Invalid base58 string");
        }

        return new SolanaAddress(value);
    }

    public override string ToString() => Value;
    public string ToShortFormatString() => ToString();
    public string ToLongFormatString() => Value.RemoveHexPrefix();

    protected override bool EqualsCore(SolanaAddress other)
        => string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);

    protected override int GetHashCodeCore() => Value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
}
