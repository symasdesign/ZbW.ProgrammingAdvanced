using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "statistics" or "hex" or "empty")) {
            Console.Error.WriteLine("Modus: all | statistics | hex | empty.");
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

        var students = new List<Student> {
            new(2009001, "John Doe", "Computing"),
            new(2009002, "Linda Miller", "Chemistry"),
            new(2009003, "Ann Foster", "Mathematics"),
            new(2009004, "Sam Dough", "Computing")
        };
        var markings = new List<Marking> {
            new(2009001, "Programming", 4), new(2009002, "Databases", 5),
            new(2009003, "Graphics", 6), new(2009004, "Chemistry", 6),
            new(2009001, "Math", 5)
        };
        Run("statistics", () => {
            foreach (var result in Operations.Statistics(students, markings))
                Console.WriteLine($"{result.Name}: {result.CourseCount} Kurse, Ø {result.AverageMark:0.0}");
        });
        Run("hex", () => {
            foreach (var text in new[] { "#123", "#ABCDEF", "EF90", "3F", "#XYz87K", "x#ABCDEFx" })
                Console.WriteLine($"{text}: {Operations.IsHexUpper(text)}");
        });
        Run("empty", () => {
            var extra = new List<Student>(students) { new(2009005, "Faye", "Computing") };
            foreach (var result in Operations.Statistics(extra, markings))
                Console.WriteLine($"{result.Name}: {result.CourseCount}, Ø {result.AverageMark?.ToString("0.0") ?? "--"}");
            var results = Operations.Statistics(extra, markings).ToList();
            var peopleAverage = results.Where(r => r.AverageMark.HasValue)
                .Average(r => r.AverageMark.GetValueOrDefault());
            var ids = extra.Where(s => s.Subject == "Computing").Select(s => s.Id).ToHashSet();
            var gradeAverage = markings.Where(m => ids.Contains(m.StudentId)).Average(m => m.Mark);
            Console.WriteLine($"Personenschnitte: {peopleAverage:0.00}; Einzelnoten: {gradeAverage:0.0}");
        });
    }
}

public sealed record Student(int Id, string Name, string Subject);
public sealed record Marking(int StudentId, string Course, int Mark);
public sealed record StudentResult(string Name, int CourseCount, double? AverageMark);
public static class Operations {
    public static IEnumerable<StudentResult> Statistics(IEnumerable<Student> students, IEnumerable<Marking> markings) {
        // TODO: GroupJoin und leere Notengruppe behandeln.
        throw new NotImplementedException();
    }
    public static bool IsHexUpper(string text) {
        // TODO: ganzes HEX-Color-Muster formulieren.
        throw new NotImplementedException();
    }
}
