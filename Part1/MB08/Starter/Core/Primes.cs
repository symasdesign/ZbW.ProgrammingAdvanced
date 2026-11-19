namespace PrimeLab;

public static class Primes {
    public static bool IsPrime(long number) {
        if (number < 2)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;
        for (long divisor = 3; divisor <= number / divisor; divisor += 2)
            if (number % divisor == 0)
                return false;
        return true;
    }
    // Genau count Primzahlen strikt grösser als start.
    public static List<long> Next(long start, int count) {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, 10000);
        var result = new List<long>(count);
        var candidate = Math.Max(start, 1);
        while (result.Count < count) {
            if (candidate == long.MaxValue)
                throw new OverflowException("Keine nächste Zahl im Wertebereich von long.");
            candidate++;
            if (IsPrime(candidate))
                result.Add(candidate);
        }
        return result;
    }
}
