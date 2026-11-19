using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "pipeline" or "sort" or "minimum")) {
            Console.Error.WriteLine("Modus: all | pipeline | sort | minimum.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        Run("pipeline", () => {
            IProcessor<string, int> parse = new ParseStep();
            IProcessor<int, int> twice = new DoubleStep();
            IProcessor<int, string> format = new FormatStep();
            Console.WriteLine(format.Process(twice.Process(parse.Process("42"))));
        });
        Run("sort", () => {
            var people = new List<Person> {
                new(2, "Beni"), new(1, "Ada"), new(3, "Cora")
            };
            Operations.Sort(people);
            foreach (var p in people)
                Console.WriteLine($"{p.Id}: {p.Name}");
        });
        Run("minimum", () => {
            Console.WriteLine(Operations.Minimum(new[] { 4, 2, 9, 2 }));
            Console.WriteLine(Operations.Minimum(new[] { "Zug", "Bern", "Basel" }));
        });
    }
}

public interface IProcessor<TIn, TOut> {
    TOut Process(TIn input);
}
public sealed class ParseStep : IProcessor<string, int> {
    public int Process(string input) => int.Parse(input, CultureInfo.InvariantCulture);
}
public sealed class DoubleStep : IProcessor<int, int> {
    public int Process(int input) => checked(input * 2);
}
public sealed class FormatStep : IProcessor<int, string> {
    public string Process(int input) => $"Value: {input}";
}
public sealed record Person(int Id, string Name) : IComparable<Person> {
    public int CompareTo(Person? other) => other is null ? 1 : Id.CompareTo(other.Id);
}
public static class Operations {
    public static void Sort<T>(List<T> items) where T : IComparable<T> {
        ArgumentNullException.ThrowIfNull(items);
        items.Sort();
    }
    public static T Minimum<T>(IReadOnlyList<T> items) where T : IComparable<T> {
        ArgumentNullException.ThrowIfNull(items);
        if (items.Count == 0)
            throw new ArgumentException("Mindestens ein Element erforderlich.");
        var best = items[0];
        for (var i = 1; i < items.Count; i++) {
            if (items[i].CompareTo(best) < 0)
                best = items[i];
        }
        return best;
    }
}
