using System.Numerics;

namespace NftFaucet.Domain.Models;

public class Balance
{
    public BigInteger Amount { get; }
    public string Currency { get; }

    public Balance(BigInteger amount, string currency)
    {
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }
}
