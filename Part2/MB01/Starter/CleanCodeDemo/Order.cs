namespace CleanCodeDemo;

public sealed record Order(int Number, string Customer, DateTime OrderDate, bool Delivered) {
    public static Order FromCustomer(int number, Customer customer, DateTime date) {
        // TODO: Kundenkennung und Auftragsdatum aus den Parametern übernehmen.
        return new Order(number, string.Empty, default, false);
    }
}
