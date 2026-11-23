using System.Reflection;
using Course.Shop;

namespace Course.Reflection;

public static class ReflectionDemo {
    public static void Inspect() {
        var counter = new Counter(12);
        var type = typeof(Counter);
        Console.WriteLine($"typeof == GetType: {type == counter.GetType()}");
        foreach (var property in type.GetProperties())
            Console.WriteLine($"Property: {property.PropertyType.Name} {property.Name}");
        foreach (var constructor in type.GetConstructors())
            foreach (var parameter in constructor.GetParameters())
                Console.WriteLine($"Parameter: {parameter.ParameterType.Name} {parameter.Name}");
    }

    public static void Construct() {
        var type = typeof(Counter);
        var constructor = type.GetConstructor([typeof(int)])
            ?? throw new InvalidOperationException("Counter(int) fehlt.");
        var counter = (Counter)constructor.Invoke([12]);
        var method = type.GetMethod(nameof(Counter.Increment), BindingFlags.Public | BindingFlags.Instance)
            ?? throw new InvalidOperationException("Increment fehlt.");
        method.Invoke(counter, null);
        Console.WriteLine($"Nach Increment: {counter.CountValue}");
    }

    public static void Graph() {
        var constructor = typeof(Shopper).GetConstructors().Single();
        foreach (var parameter in constructor.GetParameters())
            Console.WriteLine($"Shopper braucht: {parameter.ParameterType.Name}");
    }
}
