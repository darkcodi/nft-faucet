using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NftFaucet.Extensions;

public static class StringExtensions
{
    public static bool IsValidJson(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return false;

        str = str.Trim();
        if ((!str.StartsWith("{") || !str.EndsWith("}")) && (!str.StartsWith("[") || !str.EndsWith("]")))
            return false;

        try
        {
            var _ = JToken.Parse(str);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}
