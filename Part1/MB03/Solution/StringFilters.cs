namespace Course;

public delegate bool StringPredicate(string value);

public static class StringFilters {
    public static List<string> StringWhere(List<string> source, StringPredicate predicate) {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        var result = new List<string>();
        foreach (var value in source) {
            if (predicate(value))
                result.Add(value);
        }
        return result;
    }

    public static List<string> StringWhereFunc(List<string> source, Func<string, bool> predicate) {
        ArgumentNullException.ThrowIfNull(predicate);
        // Adapter: Die Signatur passt; ein neuer Delegate ruft die Func-Instanz auf.
        // Unterschiedliche Delegate-Typen sind nicht unmittelbar zuweisbar.
        return StringWhere(source, predicate.Invoke);
    }
}
