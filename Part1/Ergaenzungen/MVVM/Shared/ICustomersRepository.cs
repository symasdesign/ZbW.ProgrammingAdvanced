using Datasource;

namespace WpfExercise.Services;

public interface ICustomersRepository {
    Task<List<Customer>> GetCustomersAsync();
    Task<Customer> UpdateCustomerAsync(Customer customer);
}
