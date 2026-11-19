namespace Course;

// TODO: Zuerst selbst den passenden Delegate-Typ auf Papier entwerfen.
// Anschliessend mit dieser kompilierbaren Vorgabe vergleichen.
public delegate bool StringPredicate(string value);

public static class StringFilters {
    public static List<string> StringWhere(List<string> source, StringPredicate predicate) {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        // TODO: Neue Ergebnisliste; jedes Element genau einmal prüfen.
        // TODO: Treffer in ursprünglicher Reihenfolge hinzufügen; source nicht verändern.
        throw new NotImplementedException("StringWhere implementieren.");
    }

    public static List<string> StringWhereFunc(List<string> source, Func<string, bool> predicate) {
        ArgumentNullException.ThrowIfNull(predicate);
        // TODO: Dieselbe Semantik mit dem Standarddelegate Func<string, bool>.
        // Tipp: StringWhere wiederverwenden; predicate.Invoke ist eine Methodengruppe.
        throw new NotImplementedException("Func-Variante implementieren.");
    }
}
