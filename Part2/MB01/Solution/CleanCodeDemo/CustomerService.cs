namespace CleanCodeDemo;

public sealed class CustomerService {
    private readonly List<Customer> customers;

    public CustomerService(IEnumerable<Customer> customers) {
        this.customers = new List<Customer>(customers);
    }

    public bool ContainsCustomer(Customer customer) {
        return customers.Contains(customer);
    }

    public bool DeleteCustomer(Customer customer) {
        return customers.Remove(customer);
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
        var sorted = new List<Customer>(customers);
        sorted.Sort((a, b) => {
            var byAge = a.Age.CompareTo(b.Age);
            return byAge != 0 ? byAge : a.Id.CompareTo(b.Id);
        });
        return sorted;
    }

    public List<Customer> GetSortedByLastName() {
        var sorted = new List<Customer>(customers);
        sorted.Sort((a, b) => {
            var byName = StringComparer.Ordinal.Compare(a.LastName, b.LastName);
            return byName != 0 ? byName : a.Id.CompareTo(b.Id);
        });
        return sorted;
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
