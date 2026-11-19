namespace Course;

public sealed class Checks {
    private int passed;
    private int failed;

    public void Test(string name, Action assertion) {
        try {
            assertion();
            passed++;
            Console.WriteLine($"PASS {name}");
        } catch (Exception ex) {
            failed++;
            Console.WriteLine($"FAIL {name}: {ex.GetType().Name}: {ex.Message}");
        }
    }

    public static void Equal<T>(T expected, T actual) {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            throw new InvalidOperationException($"Erwartet: {expected}; erhalten: {actual}");
    }

    public static void Sequence<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
        if (!expected.SequenceEqual(actual))
            throw new InvalidOperationException("Sequenzen unterscheiden sich.");
    }

    public static void Throws<TException>(Action action) where TException : Exception {
        try {
            action();
        } catch (TException) { return; }
        throw new InvalidOperationException($"Erwartete Exception: {typeof(TException).Name}");
    }

    public int Finish() {
        Console.WriteLine($"Ergebnis: {passed} bestanden, {failed} fehlgeschlagen.");
        return failed == 0 ? 0 : 1;
    }
}
