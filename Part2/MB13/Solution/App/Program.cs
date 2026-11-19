using System.Text.Json;
using Course.Orders;
using Microsoft.Extensions.DependencyInjection;

internal class Program {
    private static void Main(string[] args) {
        var mode = args.FirstOrDefault() ?? "refactor";
        var data = DemoData.Create();
        if (mode == "invalid")
            data = [.. data, new Order(104, "Bad", 10m, false, "blue")];
        try {
            if (mode == "legacy") {
                LegacyReport.Run(data, 80m);
                return;
            }
            var services = new ServiceCollection();
            services.AddSingleton<IRepository<Order>>(_ => new MemoryRepository<Order>(data));
            services.AddScoped<OrderService>();
            using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<OrderService>();
            var report = service.CreateReport(80m);
            var json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            var restored = JsonSerializer.Deserialize<OrderReport[]>(json)
                ?? throw new InvalidOperationException("JSON enthielt null.");
            Console.WriteLine($"Zurückgelesene Aufträge: {restored.Length}");
        } catch (Exception exception) when (exception is ArgumentException or NotImplementedException) {
            Console.WriteLine(exception.Message);
        }
    }
}
