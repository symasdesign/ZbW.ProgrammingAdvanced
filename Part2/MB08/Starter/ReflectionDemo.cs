using Course.Shop;

namespace Course.Reflection;

public static class ReflectionDemo {
    public static void Inspect() {
        var counter = new Counter(12);
        Console.WriteLine($"Ausgangswert: {counter.CountValue}");
        // TODO: Type sowie public Properties und Konstruktorparameter ausgeben.
    }

    public static void Construct() {
        // TODO: Counter(int) per Reflection ermitteln und mit 12 aufrufen.
        // Increment per MethodInfo aufrufen; Rückgabewert und Nullfälle erklären.
        Console.WriteLine("Construct vervollständigen.");
    }

    public static void Graph() {
        // TODO: Ermittle den öffentlichen Konstruktor von Shopper und lies den Parametertyp.
        Console.WriteLine("Konstruktor von Shopper untersuchen.");
    }
}
