namespace Eratosthenes;

internal enum PrimeType { Prim, NotPrim }

internal static class Sieve {
    private static int Main(string[] args) {
        var size = 20;
        if (args.Length > 0 && (!int.TryParse(args[0], out size) || size < 0)) {
            Console.Error.WriteLine("Bitte eine nichtnegative ganze Zahl eingeben.");
            return 1;
        }

        var sieve = Eratosthenes(size);
        for (var i = 0; i < size; i++) {
            Console.Write($"{i}:{sieve[i]} ");
            if ((i + 1) % 10 == 0 || i + 1 == size) {
                Console.WriteLine();
            }
        }

        Console.WriteLine("Array: " + string.Join(", ", ExtractPrimes(sieve)));
        Console.WriteLine("Liste: " + string.Join(", ", ExtractPrimes2(sieve)));
        var dictionary = ExtractPrimes3(sieve);
        Console.WriteLine("Dictionary: " + string.Join(", ", dictionary.Select(p => $"{p.Key} -> {p.Value}")));
        return 0;
    }

    private static PrimeType[] Eratosthenes(int size) {
        var sieve = new PrimeType[size];
        for (var i = 0; i < Math.Min(size, 2); i++) {
            sieve[i] = PrimeType.NotPrim;
        }
        for (var p = 2; (long)p * p < size; p++) {
            if (sieve[p] == PrimeType.Prim) {
                for (var k = (long)p * p; k < size; k += p) {
                    sieve[k] = PrimeType.NotPrim;
                }
            }
        }
        return sieve;
    }

    private static int[] ExtractPrimes(PrimeType[] primes) {
        var nrPrimes = 0;
        for (var k = 2; k < primes.Length; k++)
            if (primes[k] == PrimeType.Prim) {
                nrPrimes++;
            }

        var ePrimes = new int[nrPrimes];
        var p = 0;
        for (var k = 2; k < primes.Length; k++) {
            if (primes[k] == PrimeType.Prim) {
                ePrimes[p++] = k;
            }
        }

        return ePrimes;
    }

    private static List<int> ExtractPrimes2(PrimeType[] primes) {
        var ePrimes = new List<int>();
        for (var k = 2; k < primes.Length; k++) {
            if (primes[k] == PrimeType.Prim) {
                ePrimes.Add(k);
            }
        }

        return ePrimes;
    }

    private static Dictionary<int, int> ExtractPrimes3(PrimeType[] primes) {
        var ePrimes = new Dictionary<int, int>();
        var p = 0;
        for (var k = 2; k < primes.Length; k++) {
            if (primes[k] == PrimeType.Prim) {
                ePrimes[p++] = k;
            }
        }

        return ePrimes;
    }
}
