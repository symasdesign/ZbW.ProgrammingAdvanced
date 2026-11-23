public sealed record DataRow(int Number, string Text);
public static class Operations {
    public static IEnumerable<string> FilterCities(IEnumerable<string> cities) {
        // TODO: B-Städte filtern und sortieren.
        throw new NotImplementedException();
    }
    public static List<DataRow> SelectRows(IEnumerable<DataRow> rows) {
        // TODO: Bedingungen, Sortierung und Materialisierung ergänzen.
        throw new NotImplementedException();
    }
    public static IEnumerable<string> QuerySyntax(IEnumerable<string> cities) {
        // TODO: gleichwertige Querysyntax schreiben.
        throw new NotImplementedException();
    }
    public static IEnumerable<StudentResult> Statistics(IEnumerable<Student> students, IEnumerable<Marking> markings) {
        // TODO: GroupJoin und leere Notengruppe behandeln.
        throw new NotImplementedException();
    }
}

public sealed record Student(int Id, string Name, string Subject);
public sealed record Marking(int StudentId, string Course, int Mark);
public sealed record StudentResult(string Name, int CourseCount, double? AverageMark);
