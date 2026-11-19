namespace ThreadLab;

public static class FileComparison {
    public static bool Equal(string firstPath, string secondPath) {
        char[]? first = null, second = null;
        Exception? firstError = null, secondError = null;
        var left = new Thread(() => { try { first = File.ReadAllText(firstPath).ToCharArray(); } catch (Exception ex) { firstError = ex; } });
        var right = new Thread(() => { try { second = File.ReadAllText(secondPath).ToCharArray(); } catch (Exception ex) { secondError = ex; } });
        left.Start();
        right.Start();
        // TODO: Erst nach beiden Join-Aufrufen sind Ergebnisse vollständig.
        left.Join();
        right.Join(); // TODO: erklärt, weshalb diese Aufrufe nötig sind.
        if (firstError is not null)
            throw new IOException("Erste Datei konnte nicht gelesen werden.", firstError);
        if (secondError is not null)
            throw new IOException("Zweite Datei konnte nicht gelesen werden.", secondError);
        // TODO: Vergleichen Sie beide vollständigen Arrays wertweise.
        return false;
    }
}

public static class Counter {
    public static int IncrementConcurrently(int threads, int iterations) {
        ArgumentOutOfRangeException.ThrowIfLessThan(threads, 1);
        ArgumentOutOfRangeException.ThrowIfNegative(iterations);
        var gate = new object();
        var value = 0;
        var workers = Enumerable.Range(0, threads).Select(_ => new Thread(() => {
            for (var i = 0; i < iterations; i++) {
                // Kurze gemeinsame Race-Aktivierung: gesamten Read-Modify-Write-Zugriff sperren.
                value++;
            }
        })).ToArray();
        foreach (var worker in workers)
            worker.Start();
        foreach (var worker in workers)
            worker.Join();
        return value;
    }
    public static int DemonstrateLostUpdate() {
        // Barrier erzwingt nur die Lehrsituation: beide lesen 0 vor beiden Schreibzugriffen.
        using var bothRead = new Barrier(2);
        var value = 0;
        Thread Make() => new(() => { var snapshot = value; bothRead.SignalAndWait(); value = snapshot + 1; });
        var a = Make();
        var b = Make();
        a.Start();
        b.Start();
        a.Join();
        b.Join();
        return value;
    }
}

public sealed class BoundedBuffer {
    // Ursprüngliche Pufferaufgabe: die Synchronisierung in Put und Get ergänzen.
    private readonly object gate = new();
    private readonly char[] buf;
    private int head, tail, count;
    public BoundedBuffer(int capacity) {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        buf = new char[capacity];
    }
    public int Count {
        get {
            lock (gate)
                return count;
        }
    }
    public void Put(char value) {
        // TODO: denselben gate-Monitor wie in Get betreten.
        // Bei vollem Puffer in einer while-Schleife mit Monitor.Wait warten.
        // Ringpuffer-Schritte: buf[tail] = value; tail = (tail + 1) % buf.Length; count++;
        // Danach wartende Threads mit Monitor.PulseAll benachrichtigen.
        throw new NotImplementedException("Put synchronisieren.");
    }
    public char Get() {
        // TODO: denselben gate-Monitor wie in Put betreten.
        // Bei leerem Puffer in einer while-Schleife mit Monitor.Wait warten.
        // Ringpuffer-Schritte: value = buf[head]; head = (head + 1) % buf.Length; count--;
        // Danach Monitor.PulseAll ausführen und das entnommene Zeichen zurückgeben.
        throw new NotImplementedException("Get synchronisieren.");
    }
}
