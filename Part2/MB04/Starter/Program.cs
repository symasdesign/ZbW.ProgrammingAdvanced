using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "iterator" or "independent" or "cities" or "filter" or "fibonacci")) {
            Console.Error.WriteLine("Modus: all | iterator | independent | cities | filter | fibonacci.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            try {
                action();
            } catch (NotImplementedException) {
                Console.WriteLine("TODO: Aufgabenmethode vervollständigen.");
            }
        }

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
        Run("cities", () => {
            var cities = new YieldCityCollection();
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
