using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "cities" or "filter" or "fibonacci")) {
            Console.Error.WriteLine("Modus: all | cities | filter | fibonacci.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        Run("cities", () => {
            var cities = new CityCollection();
            Console.WriteLine(string.Join(", ", cities));
            Console.WriteLine(string.Join(", ", cities.Reverse));
        });
        Run("filter", () => {
            var values = new List<int> { 1, 2, 3, 4, 5, 6 };
            var calls = 0;
            IEnumerable<int> query = values.ZbwWhere(n => { calls++; return n % 2 == 0; });
            Console.WriteLine($"Vor Enumeration: {calls}");
            Console.WriteLine(string.Join(", ", query));
            Console.WriteLine($"Danach: {calls}");
            Console.WriteLine(string.Join(", ", query));
            Console.WriteLine($"Nach zweitem Durchlauf: {calls}");
        });
        Run("fibonacci", () => Console.WriteLine(string.Join(", ", Sequences.Fibonacci(8))));
    }
}

public sealed class CityCollection : IEnumerable<string> {
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
        foreach (T item in source) {
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
