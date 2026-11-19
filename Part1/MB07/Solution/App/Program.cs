using ThreadLab;
using System.Diagnostics;

internal class Program {
    private static void Main(string[] args) {
        Console.WriteLine($"Prozess {Environment.ProcessId}, Hauptthread {Environment.CurrentManagedThreadId}");
        if (args.Length == 2) {
            try {
                Console.WriteLine(FileComparison.Equal(args[0], args[1]) ? "Inhalt gleich" : "Inhalt verschieden");
            } catch (IOException ex) { Console.WriteLine(ex.Message); Environment.ExitCode = 1; }
            return;
        }
        Console.WriteLine($"Erzwungenes verlorenes Update: {Counter.DemonstrateLostUpdate()} (korrekt wäre 2)");
        Console.WriteLine($"4 × 100000 Erhöhungen: {Counter.IncrementConcurrently(4, 100000)} (Soll: 400000)");
        var buffer = new BoundedBuffer(4);
        const string input = "ABCDEFGHIJKL";
        var received = new List<char>();
        var producer = new Thread(() => { foreach (var c in input) buffer.Put(c); });
        var consumer = new Thread(() => { for (var i = 0; i < input.Length; i++) received.Add(buffer.Get()); });
        producer.Start();
        consumer.Start();
        producer.Join();
        consumer.Join();
        Console.WriteLine($"Buffer FIFO: {new string(received.ToArray())}; Rest={buffer.Count}");
    }
}
