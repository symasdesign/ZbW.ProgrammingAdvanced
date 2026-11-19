public interface IPair {
    object? First { get; }
    object? Second { get; }
}

public sealed class OrderedPair(object? first, object? second) : IPair {
    public object? First { get; } = first;
    public object? Second { get; } = second;

    // TODO: Gleichheit und Hashcode für geordnete Paare.
}

public sealed class UnorderedPair(object? first, object? second) : IPair {
    public object? First { get; } = first;
    public object? Second { get; } = second;

    // TODO: Gleichheit und Hashcode für ungeordnete Paare.
}
