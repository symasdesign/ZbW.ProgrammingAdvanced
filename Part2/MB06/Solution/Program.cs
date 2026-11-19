using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "cities" or "rows" or "query")) {
            Console.Error.WriteLine("Modus: all | cities | rows | query.");
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
            var cities = new List<string> { "Bern", "Basel", "Zürich", "Genf", "Biel" };
            IEnumerable<string> query = Operations.FilterCities(cities);
            Console.WriteLine(string.Join(", ", query));
            var snapshot = query.ToList();
            cities.Add("Baden");
            Console.WriteLine($"Query: {string.Join(", ", query)}");
            Console.WriteLine($"Liste: {string.Join(", ", snapshot)}");
        });
        Run("rows", () => {
            var rows = new List<DataRow> {
                new(2, "fix"), new(5, "fix"), new(6, "fix"),
                new(3, "fix"), new(4, "other"), new(0, "fix")
            };
            foreach (var row in Operations.SelectRows(rows))
                Console.WriteLine($"{row.Number}: {row.Text}");
        });
        Run("query", () => {
            var cities = new[] { "Bern", "Basel", "Zürich", "Genf", "Biel" };
            Console.WriteLine(string.Join(", ", Operations.QuerySyntax(cities)));
        });
    }
}

public sealed record DataRow(int Number, string Text);
public static class Operations {
    public static IEnumerable<string> FilterCities(IEnumerable<string> cities) {
        return cities.Where(c => c.StartsWith("B", StringComparison.Ordinal))
                     .OrderBy(c => c, StringComparer.Ordinal);
    }
    public static List<DataRow> SelectRows(IEnumerable<DataRow> rows) {
        return rows.Where(r => (r.Number * r.Number < 25 || r.Number % 2 != 0)
                                && r.Text == "fix")
                   .OrderBy(r => r.Text, StringComparer.Ordinal)
                   .ThenBy(r => r.Number)
                   .ToList();
    }
    public static IEnumerable<string> QuerySyntax(IEnumerable<string> cities) {
        return from city in cities
               where city.StartsWith("B", StringComparison.Ordinal)
               orderby city
               select city;
    }
}
