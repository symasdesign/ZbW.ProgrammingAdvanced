using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "json" or "export" or "invalid")) {
            Console.Error.WriteLine("Modus: all | json | export | invalid.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            try {
                action();
            } catch (NotImplementedException) {
                Console.WriteLine("TODO: Aufgabenmethode vervollständigen.");
            }
        }

        var order = new Order {
            Number = 42,
            Customer = "Ada",
            OrderDate = new DateTime(2026, 10, 11),
            Delivered = true
        };
        Run("json", () => {
            var json = Operations.SerializeOrder(order);
            Console.WriteLine(json);
            var copy = Operations.DeserializeOrder(json);
            Console.WriteLine($"Kopie: {copy.Number}, {copy.Customer}, {copy.Delivered}");
        });
        Run("export", () => {
            var json = Operations.Export(order);
            Console.WriteLine(json);
            var copy = JsonSerializer.Deserialize<OrderExport>(json)
                ?? throw new JsonException("Keine Bestellung.");
            Console.WriteLine($"Ignorierte Property: {copy.Delivered}");
        });
        Run("invalid", () => {
            foreach (var json in new[] { "null", "{ broken", "{}" }) {
                try {
                    var copy = Operations.DeserializeOrder(json);
                    Console.WriteLine($"{copy.Number}|{copy.Customer}|{copy.OrderDate:O}|{copy.Delivered}");
                } catch (JsonException) { Console.WriteLine("JSON enthält keine lesbare Bestellung."); }
            }
        });
    }
}

public sealed class Order {
    public int Number {
        get; set;
    }
    public string Customer { get; set; } = string.Empty;
    public DateTime OrderDate {
        get; set;
    }
    public bool Delivered {
        get; set;
    }
}
public sealed class OrderExport {
    public int Number {
        get; set;
    }
    public string Customer { get; set; } = string.Empty;
    public DateTime OrderDate {
        get; set;
    }
    public bool Delivered {
        get; set;
    }
}
public static class Operations {
    public static string SerializeOrder(Order order) {
        // TODO: eingerücktes JSON schreiben.
        throw new NotImplementedException();
    }
    public static Order DeserializeOrder(string json) {
        // TODO: JSON lesen und null abweisen.
        throw new NotImplementedException();
    }
    public static string Export(Order order) {
        // TODO: OrderExport füllen und serialisieren.
        throw new NotImplementedException();
    }
}
