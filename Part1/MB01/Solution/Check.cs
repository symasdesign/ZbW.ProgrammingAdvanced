namespace Course;

public static class Check {
    private static int passed, failed;
    public static void Run(string name, Action test) {
        try {
            test();
            passed++;
            Console.WriteLine($"PASS {name}");
        } catch (Exception e) {
            failed++;
            Console.WriteLine($"FAIL {name}: {e.Message}");
        }
    }
    public static void True(bool condition) {
        if (!condition)
            throw new InvalidOperationException("Erwartet: true.");
    }
    public static void Equal<T>(T expected, T actual) {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            throw new InvalidOperationException($"Erwartet {expected}; erhalten {actual}.");
    }
    public static void Throws<T>(Action action) where T : Exception {
        try {
            action();
        } catch (T) { return; }
        throw new InvalidOperationException($"Erwartet: {typeof(T).Name}.");
    }
    public static int Finish() {
        Console.WriteLine($"{passed} bestanden, {failed} fehlgeschlagen.");
        return failed == 0 ? 0 : 1;
    }
}
