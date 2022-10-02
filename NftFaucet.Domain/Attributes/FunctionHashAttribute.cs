namespace NftFaucet.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class FunctionHashAttribute : Attribute
{
    public string Hash { get; set; }

    public FunctionHashAttribute(string hash)
    {
        if (string.IsNullOrEmpty(hash))
            throw new ArgumentNullException(nameof(hash));

        Hash = hash;
    }
}
