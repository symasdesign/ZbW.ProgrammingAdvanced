namespace CleanCodeDemo;

public sealed record Order(int Number, string Customer, DateTime OrderDate, bool Delivered) {
    public static Order FromCustomer(int number, Customer customer, DateTime date) {
        return new Order(number, customer.LoginName, date, false);
    }
}
