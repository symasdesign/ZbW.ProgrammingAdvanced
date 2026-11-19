namespace Course.Reflection;

public sealed class Counter {
    public Counter(int start) => CountValue = start;
    public int CountValue {
        get; private set;
    }
    public void Increment() => CountValue++;
}
