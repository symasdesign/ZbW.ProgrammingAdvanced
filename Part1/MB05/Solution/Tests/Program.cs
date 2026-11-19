using Datasource;
using System.Collections.Specialized;

internal class Program {
    private static void Main(string[] args) {
        var failed = 0;
        void Check(bool condition, string name) {
            Console.WriteLine($"{(condition ? "PASS" : "FAIL")} {name}");
            if (!condition)
                failed++;
        }
        var data = Data.Load(Path.Combine(AppContext.BaseDirectory, "CustomersOrders.xml"));
        Check(data.Customers.CustomerList.Count > 1, "Originaldaten laden mehrere Kunden");
        Check(data.Orders.OrderList.Count > 0, "Bestellungen bleiben erhalten");
        var customer = data.Customers.CustomerList[0];
        customer.CompanyName = "Rundreise-Test";
        var path = Path.Combine(Path.GetTempPath(), $"pga-test-{Guid.NewGuid():N}.xml");
        try {
            data.Save(path);
            var copy = Data.Load(path);
            Check(copy.Customers.CustomerList[0].CompanyName == "Rundreise-Test", "XML-Rundreise erhält Änderungen");
            Check(copy.Orders.OrderList.Count == data.Orders.OrderList.Count, "XML-Rundreise erhält Bestellungen");
        } finally { if (File.Exists(path)) File.Delete(path); }

        Environment.ExitCode = failed == 0 ? 0 : 1;
    }
}
