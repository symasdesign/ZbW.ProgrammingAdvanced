using System.IO;
using Datasource;

namespace WpfExercise.Services;

public sealed class CustomersRepository : ICustomersRepository {
    public static string DataPath => Path.Combine(AppContext.BaseDirectory, "CustomersOrders.xml");

    public Task<List<Customer>> GetCustomersAsync() {
        return Task.Run(() => Data.Load(DataPath).Customers.CustomerList.ToList());
    }

    public Task<Customer> UpdateCustomerAsync(Customer customer) {
        return Task.Run(() => {
            var data = Data.Load(DataPath);
            var customers = data.Customers.CustomerList;
            var index = customers.IndexOf(customer);
            if (index >= 0) {
                customers[index] = customer;
            } else {
                customers.Add(customer);
            }
            data.Save(DataPath);
            return customer;
        });
    }
}
