using PrimeLab;

internal class Program {
    private static async global::System.Threading.Tasks.Task Main(string[] args) {
        var failed = 0;
        void Check(bool ok, string name) {
            Console.WriteLine($"{(ok ? "PASS" : "FAIL")} {name}");
            if (!ok)
                failed++;
        }
        foreach (var n in new long[] { -7, 0, 1, 4, 9, 25, 49, 121 })
            Check(!Primes.IsPrime(n), $"{n} ist nicht prim");
        foreach (var n in new long[] { 2, 3, 5, 97, 997, 2147483647 })
            Check(Primes.IsPrime(n), $"{n} ist prim");
        Check(Primes.Next(0, 5).SequenceEqual(new long[] { 2, 3, 5, 7, 11 }), "Anzahl bedeutet Trefferzahl");
        Check(Primes.Next(11, 3).SequenceEqual(new long[] { 13, 17, 19 }), "Startgrenze ist exklusiv");
        Check(Primes.Next(50, 0).Count == 0, "Null gewünschte Treffer");
        try {
            Primes.Next(1, -1);
            Check(false, "Negative Anzahl abweisen");
        } catch (ArgumentOutOfRangeException) { Check(true, "Negative Anzahl abweisen"); }
        try {
            Primes.Next(long.MaxValue, 1);
            Check(false, "Wertebereich schützen");
        } catch (OverflowException) { Check(true, "Wertebereich schützen"); }
        var expected = Primes.Next(10000, 20);
        var asyncResult = await Task.Run(() => Primes.Next(10000, 20));
        Check(expected.SequenceEqual(asyncResult), "Auslagerung ändert Ergebnisse nicht");
        Environment.ExitCode = failed == 0 ? 0 : 1;
    }
}
