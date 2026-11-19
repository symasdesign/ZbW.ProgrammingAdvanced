namespace Course.Orders;

public sealed record Order(int Number, string Customer, decimal Total,
    bool Delivered, string Color);
public sealed record OrderReport(int Number, string Customer, decimal Total, string Color);

public interface IRepository<out T> {
    IEnumerable<T> All();
}

public sealed class MemoryRepository<T>(IEnumerable<T> source) : IRepository<T> {
    private readonly T[] items = source.ToArray();
    public IEnumerable<T> All() {
        foreach (var item in items)
            yield return item;
    }
}

public static class DemoData {
    public static Order[] Create() =>
    [
        new(101, "Ada", 120m, false, "#336699"),
        new(102, "Linus", 50m, true, "#FFF"),
        new(103, "Grace", 80m, false, "#CC3300")
    ];
}
