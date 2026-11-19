using Course;

internal class Program {
    private static int Main(string[] args) {
        if (args.Length > 0 && args[0] == "--self-test")
            return SelfTests.Run(args.Length > 1 ? args[1] : "all");

        var words = new List<string> { "Bern", "Basel", "Baden", "Zug", "Biel" };
        try {
            var fourLetters = StringFilters.StringWhere(words, HasFourLetters);
            var withA = StringFilters.StringWhereFunc(words, text => text.Contains('a'));
            Console.WriteLine("Länge 4: " + string.Join(", ", fourLetters));
            Console.WriteLine("Enthält a: " + string.Join(", ", withA));
            Console.WriteLine("Original: " + string.Join(", ", words));
        } catch (NotImplementedException ex) {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Öffnen Sie StringFilters.cs; prüfen mit --self-test core.");
        }
        return 0;

        static bool HasFourLetters(string value) => value.Length == 4;
    }
}
