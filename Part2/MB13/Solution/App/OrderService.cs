using System.Text.RegularExpressions;

namespace Course.Orders;

public sealed class OrderService(IRepository<Order> repository) {
    private const string HexColor = @"\A#(?:[0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})\z";

    public OrderReport[] CreateReport(decimal minimumTotal) {
        if (minimumTotal < 0)
            throw new ArgumentOutOfRangeException(nameof(minimumTotal));
        var orders = repository.All().ToArray();
        foreach (var order in orders)
            Validate(order);
        return orders.Where(order => IsOpenOrderAtLeast(order, minimumTotal))
            .OrderBy(order => order.Number)
            .Select(order => new OrderReport(order.Number, order.Customer, order.Total, order.Color))
            .ToArray();
    }

    private static bool IsOpenOrderAtLeast(Order order, decimal minimumTotal) {
        return !order.Delivered && order.Total >= minimumTotal;
    }

    private static void Validate(Order order) {
        if (string.IsNullOrWhiteSpace(order.Customer))
            throw new ArgumentException($"Auftrag {order.Number}: Kunde fehlt.");
        if (!Regex.IsMatch(order.Color, HexColor, RegexOptions.None, TimeSpan.FromMilliseconds(100)))
            throw new ArgumentException($"Auftrag {order.Number}: Farbe ist keine HEX-Color.");
    }
}
