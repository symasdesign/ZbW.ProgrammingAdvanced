using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "greeting" or "iterator" or "independent")) {
            Console.Error.WriteLine("Modus: all | greeting | iterator | independent.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        Run("greeting", () => {
            Console.WriteLine(Operations.Greeting(null));
            Console.WriteLine(Operations.Greeting("Ada"));
        });
        Run("iterator", () => {
            var cities = new CityCollection();
            using var it = cities.GetEnumerator();
            while (it.MoveNext())
                Console.WriteLine(it.Current);
            Console.WriteLine("foreach:");
            foreach (var city in cities)
                Console.WriteLine(city);
        });
        Run("independent", () => {
            var cities = new CityCollection();
            using var first = cities.GetEnumerator();
            using var second = cities.GetEnumerator();
            first.MoveNext();
            first.MoveNext();
            second.MoveNext();
            Console.WriteLine($"Erster: {first.Current}; zweiter: {second.Current}");
        });
    }
}

public static class Operations {
    public static string Greeting(string? name) {
        var displayName = name ?? "Gast";
        return $"Hallo {displayName.ToUpperInvariant()}";
    }
}
public sealed class CityCollection : IEnumerable<string> {
    private readonly string[] cities = { "Bern", "Basel", "Zürich", "Rapperswil", "Genf" };
    public IEnumerator<string> GetEnumerator() => new CityEnumerator(cities);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
public sealed class CityEnumerator : IEnumerator<string> {
    private readonly string[] cities;
    private int position = -1;
    public CityEnumerator(string[] cities) => this.cities = cities;
    public string Current {
        get {
            if (position < 0 || position >= cities.Length)
                throw new InvalidOperationException("Kein aktuelles Element.");
            return cities[position];
        }
    }
    object System.Collections.IEnumerator.Current => Current;
    public bool MoveNext() {
        if (position < cities.Length)
            position++;
        return position < cities.Length;
    }
    public void Reset() => position = -1;
    public void Dispose() {
    }
}
