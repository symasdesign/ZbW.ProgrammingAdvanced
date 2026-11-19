using CleanCodeDemo;

internal class Program {
    private static void Main(string[] args) {
        var customers = new List<Customer> {
            new(1, "alice", "Meier", 12),
            new(2, "bob", "Meier", 9),
            new(3, "clara", "Graf", 10)
        };
        var service = new CustomerService(customers);
        Console.WriteLine("Ausgangslage: nach Text sortiert");
        service.PrintTextSortedByAge();
        Console.WriteLine("Dieselbe Referenz: " + service.ContainsCustomer(customers[0]));
        Console.WriteLine("Gleiche Id, andere Referenz: " +
            service.ContainsCustomer(new Customer(1, "alice", "Meier", 12)));
        Console.WriteLine("Sortierung nach Alter");
        CustomerService.PrintCustomers(service.GetSortedByAge());
        Console.WriteLine("Alle nach Nachname sortiert");
        CustomerService.PrintCustomers(service.GetSortedByLastName());
        Console.WriteLine("Ursprüngliche Einfügereihenfolge");
        CustomerService.PrintCustomers(customers);
        Console.WriteLine("SortedSet nach Nachname: " + service.CountByLastName());
        Console.WriteLine("Löschen vorhandene Referenz: " + service.DeleteCustomer(customers[0]));
        Console.WriteLine("Löschen erneut: " + service.DeleteCustomer(customers[0]));
    }
}
