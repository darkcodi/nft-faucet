namespace NftFaucet.Models.Enums;

public class EnumWrapper<T> where T : Enum
{
    public T Value { get; set; }
    public string ValueString { get; set; }
    public string Description { get; set; }

    public EnumWrapper(T value, string description)
    {
        Value = value;
        ValueString = value.ToString();
        Description = description;
    }
}
