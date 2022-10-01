namespace PracticalTest.Domain.Write.Common;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
    {
        if (list == null) return true;

        return !list.Any();
    }
}