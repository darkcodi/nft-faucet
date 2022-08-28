namespace NftFaucetRadzen.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> source)
        => source.GroupBy(x => x).Where(kvp => kvp.Count() > 1).Select(kvp => kvp.Key);
}
