using System.Text.Json;
using System.Text.RegularExpressions;

namespace Course.Orders;

// Vorbereitete Ausgangslage: Fachlogik, Darstellung und Ausgabe sind vermischt.
public static class LegacyReport {
    public static string Run(IEnumerable<Order> x, decimal y) {
        if (y < 0)
            throw new ArgumentOutOfRangeException(nameof(y));
        var a = x.ToArray();
        var b = new List<OrderReport>();
        foreach (var z in a) {
            if (string.IsNullOrWhiteSpace(z.Customer))
                throw new ArgumentException($"Auftrag {z.Number}: Kunde fehlt.");
            if (!Regex.IsMatch(z.Color, @"\A#(?:[0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})\z",
                RegexOptions.None, TimeSpan.FromMilliseconds(100)))
                throw new ArgumentException($"Auftrag {z.Number}: Farbe ist keine HEX-Color.");
            if (!z.Delivered)
                if (z.Total >= y)
                    b.Add(new OrderReport(z.Number, z.Customer, z.Total, z.Color));
        }
        var s = JsonSerializer.Serialize(b.OrderBy(z => z.Number).ToArray(),
            new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(s);
        return s;
    }
}
