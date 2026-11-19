namespace Course.Lifetimes;

public sealed class UsageCounter : IDisposable {
    private static int next;
    public int Id { get; } = Interlocked.Increment(ref next);
    public void Dispose() => Console.WriteLine($"Dispose Counter {Id}");
}
