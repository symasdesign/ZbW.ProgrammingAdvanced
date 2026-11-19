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
            try {
                action();
            } catch (NotImplementedException) {
                Console.WriteLine("TODO: Aufgabenmethode vervollständigen.");
            }
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
    public int Process(string input) => throw new NotImplementedException();
}
public sealed class DoubleStep : IProcessor<int, int> {
    public int Process(int input) => throw new NotImplementedException();
}
public sealed class FormatStep : IProcessor<int, string> {
    public string Process(int input) => throw new NotImplementedException();
}
public sealed record Person(int Id, string Name) : IComparable<Person> {
    public int CompareTo(Person? other) => other is null ? 1 : Id.CompareTo(other.Id);
}
public static class Operations {
    public static void Sort<T>(List<T> items) where T : IComparable<T> {
        // TODO: Constraint und Sortierung erklären und implementieren.
        throw new NotImplementedException();
    }
    public static T Minimum<T>(IReadOnlyList<T> items) where T : IComparable<T> {
        // TODO: Minimum mit CompareTo ermitteln.
        throw new NotImplementedException();
    }
}
