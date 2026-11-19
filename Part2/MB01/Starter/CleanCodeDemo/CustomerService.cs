namespace CleanCodeDemo;

public sealed class CustomerService {
    private readonly List<Customer> customers;

    public CustomerService(IEnumerable<Customer> customers) {
        this.customers = new List<Customer>(customers);
    }

    public bool ContainsCustomer(Customer customer) {
        if (customers.Contains(customer) == true) {
            return true;
        } else {
            return false;
        }
    }

    public bool DeleteCustomer(Customer customer) {
        // Check whether the customer exists.
        if (customers.Contains(customer)) {
            // Remove and return success.
            customers.Remove(customer);
            return true;
        }

        return false;
    }

    public void PrintTextSortedByAge() {
        var lines = new List<string>();
        foreach (var customer in customers) {
            lines.Add($"{customer.Age} {customer.LoginName}");
        }

        lines.Sort();
        foreach (var line in lines) {
            Console.WriteLine(line);
        }
    }

    public List<Customer> GetSortedByAge() {
        // TODO: Numerisch nach Alter, dann nach Id sortieren.
        return new List<Customer>(customers);
    }

    public List<Customer> GetSortedByLastName() {
        // TODO: Nach Nachname, dann nach Id sortieren.
        return new List<Customer>(customers);
    }

    public int CountByLastName() {
        var byLastName = Comparer<Customer>.Create((a, b) =>
            StringComparer.Ordinal.Compare(a.LastName, b.LastName));
        var unique = new SortedSet<Customer>(customers, byLastName);
        return unique.Count;
    }

    public static void PrintCustomers(IEnumerable<Customer> items) {
        foreach (var customer in items) {
            Console.WriteLine(customer);
        }
    }
}
