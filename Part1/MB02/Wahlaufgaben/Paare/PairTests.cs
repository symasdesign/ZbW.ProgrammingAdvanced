internal static class PairTests {
    private static int Main() {
        var failures = 0;
        void Check(string name, bool actual) {
            Console.WriteLine($"{(actual ? "PASS" : "FAIL")} {name}");
            if (!actual) {
                failures++;
            }
        }

        var ordered = new OrderedPair(1, 2);
        var same = new OrderedPair(1, 2);
        var reverse = new OrderedPair(2, 1);
        var unordered = new UnorderedPair(1, 2);
        var swapped = new UnorderedPair(2, 1);
        Check("Eigenschaften", Equals(ordered.First, 1) && Equals(ordered.Second, 2));
        Check("Geordnete Paare", ordered.Equals(same) && same.Equals(ordered));
        Check("Hashcode geordneter Paare", ordered.GetHashCode() == same.GetHashCode());
        Check("Reihenfolge", !ordered.Equals(reverse));
        Check("Ungeordnete Paare", unordered.Equals(swapped) && swapped.Equals(unordered));
        Check("Hashcode ungeordneter Paare", unordered.GetHashCode() == swapped.GetHashCode());
        Check("Unterschiedliche Paartypen", !ordered.Equals(unordered) && !unordered.Equals(ordered));
        return failures == 0 ? 0 : 1;
    }
}
