namespace Course;

public static class SelfTests {
    public static int Run(string group) {
        if (group is not ("all" or "core" or "independent" or "homework")) {
            Console.WriteLine("Gruppen: core, independent, homework, all");
            return 2;
        }
        var c = new Checks();
        if (group is "all" or "core") {
            c.Test("Länge 4", () => Checks.Sequence(new[] { "Bern", "Biel" },
                StringFilters.StringWhere(Words(), HasFourLetters)));
            c.Test("Zeichen a", () => Checks.Sequence(new[] { "Basel", "Baden" },
                StringFilters.StringWhere(Words(), text => text.Contains('a'))));
            c.Test("Eingabe unverändert", () => {
                var source = Words();
                var before = source.ToArray();
                _ = StringFilters.StringWhere(source, _ => false);
                Checks.Sequence(before, source);
            });
            c.Test("Neue Ergebnisliste", () => {
                var source = Words();
                var result = StringFilters.StringWhere(source, _ => true);
                Checks.Equal(false, ReferenceEquals(source, result));
                result.Clear();
                Checks.Equal(5, source.Count);
            });
            c.Test("Leere Eingabe", () => Checks.Equal(0,
                StringFilters.StringWhere([], _ => true).Count));
            c.Test("Reihenfolge und Duplikate", () => Checks.Sequence(new[] { "aa", "aa" },
                StringFilters.StringWhere(["aa", "b", "aa"], text => text.Length == 2)));
            c.Test("Genau ein Aufruf je Element", () => {
                var calls = 0;
                _ = StringFilters.StringWhere(Words(), _ => { calls++; return true; });
                Checks.Equal(5, calls);
            });
            c.Test("Null-Quelle", () => Checks.Throws<ArgumentNullException>(() =>
                StringFilters.StringWhere(null!, _ => true)));
            c.Test("Null-Prädikat", () => Checks.Throws<ArgumentNullException>(() =>
                StringFilters.StringWhere(Words(), null!)));
        }
        if (group is "all" or "independent") {
            c.Test("Func-Filter", () => Checks.Sequence(new[] { "Basel", "Baden" },
                StringFilters.StringWhereFunc(Words(), text => text.Length == 5)));
            c.Test("Block-Lambda", () => Checks.Sequence(new[] { "Bern", "Biel" },
                StringFilters.StringWhereFunc(Words(), text => {
                    var length = text.Length;
                    return length == 4;
                })));
            c.Test("Keine Treffer", () => Checks.Equal(0,
                StringFilters.StringWhereFunc(Words(), _ => false).Count));
            c.Test("Leerer String ist ein Element", () => Checks.Sequence(new[] { "" },
                StringFilters.StringWhereFunc(["", "Bern"], text => text.Length == 0)));
            c.Test("Func lässt Original unverändert", () => {
                var source = Words();
                var before = source.ToArray();
                var result = StringFilters.StringWhereFunc(source, _ => true);
                Checks.Sequence(before, source);
                Checks.Equal(false, ReferenceEquals(source, result));
            });
            c.Test("Closure liest aktuellen Grenzwert", () => {
                var limit = 4;
                Func<string, bool> predicate = text => text.Length == limit;
                limit = 5;
                Checks.Sequence(new[] { "Basel", "Baden" },
                    StringFilters.StringWhereFunc(Words(), predicate));
            });
        }
        if (group is "all" or "homework") {
            c.Test("Alter über 30", () => Checks.Sequence(new[] { 2, 4 },
                EmployeeFilters.Filter(Employees(), p => p.Age > 30).Select(p => p.Id)));
            c.Test("Gehalt über 4000", () => Checks.Sequence(new[] { 2, 5 },
                EmployeeFilters.Filter(Employees(), p => p.Salary > 4000m).Select(p => p.Id)));
            c.Test("Gehalt 2000 bis 3000 inklusive", () => Checks.Sequence(new[] { 1, 3, 4 },
                EmployeeFilters.Filter(Employees(), p => p.Salary >= 2000m && p.Salary <= 3000m)
                    .Select(p => p.Id)));
            c.Test("Objekte werden nicht kopiert", () => {
                var source = Employees();
                var result = EmployeeFilters.Filter(source, _ => true);
                Checks.Equal(false, ReferenceEquals(source, result));
                Checks.Equal(true, ReferenceEquals(source[0], result[0]));
            });
        }
        return c.Finish();
    }

    private static bool HasFourLetters(string value) => value.Length == 4;
    private static List<string> Words() => ["Bern", "Basel", "Baden", "Zug", "Biel"];
    private static List<Employee> Employees() =>
    [
        new(1, "Anna", 25, 2000m), new(2, "Beni", 40, 5000m),
        new(3, "Cora", 30, 3000m), new(4, "Dario", 31, 2500m),
        new(5, "Eva", 29, 4500m)
    ];
}
