public sealed class YieldCityCollection : IEnumerable<string> {
    private readonly string[] cities = { "Bern", "Basel", "Zürich", "Rapperswil", "Genf" };
    public IEnumerator<string> GetEnumerator() {
        foreach (var city in cities)
            yield return city;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerable<string> Reverse {
        get {
            for (var i = cities.Length - 1; i >= 0; i--)
                yield return cities[i];
        }
    }
}
public static class QueryExtensions {
    public static IEnumerable<T> ZbwWhere<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        foreach (var item in source) {
            if (predicate(item))
                yield return item;
        }
    }
}
public static class Sequences {
    public static IEnumerable<int> Fibonacci(int count) {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        var a = 0;
        var b = 1;
        for (var i = 0; i < count; i++) {
            yield return a;
            if (i + 1 < count)
                (a, b) = (b, checked(a + b));
        }
    }
}
