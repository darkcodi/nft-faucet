using System.Reflection;

namespace NftFaucetRadzen.Extensions;

public static class TypeExtensions
{
    public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo)
        where TAttribute : Attribute
    {
        var customAttributes = memberInfo?.GetCustomAttributes(false);
        var attribute = customAttributes?.OfType<TAttribute>().SingleOrDefault();
        return attribute;
    }
}
