using ThreadLab;

internal class Program {
    private static void Main(string[] args) {
        var failed = 0;
        void Check(bool ok, string name) {
            Console.WriteLine($"{(ok ? "PASS" : "FAIL")} {name}");
            if (!ok)
                failed++;
        }
        var dir = Path.Combine(Path.GetTempPath(), $"pga-threads-{Guid.NewGuid():N}");
        Directory.CreateDirectory(dir);
        try {
            var a = Path.Combine(dir, "a.txt");
            var b = Path.Combine(dir, "b.txt");
            File.WriteAllText(a, "Äpfel\nBirnen");
            File.WriteAllText(b, "Äpfel\nBirnen");
            Check(FileComparison.Equal(a, b), "Gleiche Inhalte mit Umlauten");
            File.WriteAllText(b, "Äpfel\nBirne");
            Check(!FileComparison.Equal(a, b), "Unterschied am Ende");
            File.WriteAllText(a, "");
            File.WriteAllText(b, "");
            Check(FileComparison.Equal(a, b), "Zwei leere Dateien");
            try {
                FileComparison.Equal(a, Path.Combine(dir, "missing"));
                Check(false, "Lesefehler weitergeben");
            } catch (IOException) { Check(true, "Lesefehler nach Join an Hauptthread weitergeben"); }
        } finally { Directory.Delete(dir, true); }
        Check(Counter.DemonstrateLostUpdate() == 1, "Erzwungenes Interleaving macht verlorenes Update sichtbar");
        Check(Counter.IncrementConcurrently(8, 50000) == 400000, "Geschützter Zähler erhält sämtliche Erhöhungen");
        var buffer = new BoundedBuffer(4);
        var received = new List<char>();
        const string input = "ABCDEFGHIJKLMNOP";
        var producer = new Thread(() => { foreach (var c in input) buffer.Put(c); }) { IsBackground = true };
        var consumer = new Thread(() => { for (var i = 0; i < input.Length; i++) received.Add(buffer.Get()); }) { IsBackground = true };
        producer.Start();
        consumer.Start();
        var finished = producer.Join(5000) && consumer.Join(5000);
        Check(finished, "Buffer terminiert bei gleicher Anzahl Put/Get");
        if (finished) {
            Check(new string(received.ToArray()) == input, "FIFO ohne Verlust über Ringumlauf");
            Check(buffer.Count == 0, "Leer nach vollständigem Lesen");
        }
        Environment.ExitCode = failed == 0 ? 0 : 1;
    }
}
