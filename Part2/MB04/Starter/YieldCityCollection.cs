public sealed class YieldCityCollection : IEnumerable<string> {
    private readonly string[] cities = { "Bern", "Basel", "Zürich", "Rapperswil", "Genf" };
    public IEnumerator<string> GetEnumerator() {
        // TODO: alle Städte mit yield return liefern.
        throw new NotImplementedException();
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerable<string> Reverse {
        get {
            throw new NotImplementedException(); // TODO: Rückwärtsfolge mit yield
        }
    }
}
public static class QueryExtensions {
    public static IEnumerable<T> ZbwWhere<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        // TODO: Predicate prüfen und passende Elemente liefern.
        throw new NotImplementedException();
    }
}
public static class Sequences {
    public static IEnumerable<int> Fibonacci(int count) {
        // TODO: die ersten count Fibonacci-Werte liefern.
        throw new NotImplementedException();
    }
}
