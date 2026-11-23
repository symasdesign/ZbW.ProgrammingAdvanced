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
    public static IEnumerable<StudentResult> Statistics(IEnumerable<Student> students, IEnumerable<Marking> markings) {
        return students.Where(s => s.Subject == "Computing")
            .GroupJoin(markings, s => s.Id, m => m.StudentId, (student, group) => {
                var grades = group.ToList();
                double? average = grades.Count == 0 ? null : grades.Average(m => m.Mark);
                return new StudentResult(student.Name, grades.Count, average);
            })
            .OrderByDescending(s => s.AverageMark)
            .ThenBy(s => s.Name, StringComparer.Ordinal);
    }
}

public sealed record Student(int Id, string Name, string Subject);
public sealed record Marking(int StudentId, string Course, int Mark);
public sealed record StudentResult(string Name, int CourseCount, double? AverageMark);
