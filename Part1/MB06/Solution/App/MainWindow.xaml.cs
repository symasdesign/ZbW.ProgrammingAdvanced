using System.IO;
using System.Windows;
using System.Windows.Controls;
using Datasource;
namespace CustomerApp;

public partial class MainWindow : Window {
    private Data? _data;
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "CustomersOrders.xml");
    public MainWindow() => InitializeComponent();

    private void Window_Loaded(object sender, RoutedEventArgs e) {
        try {
            _data = Data.Load(_path);
            CustomersList.ItemsSource = _data.Customers.CustomerList;
            CustomersList.SelectedIndex = _data.Customers.CustomerList.Count > 0 ? 0 : -1;
            StatusText.Text = $"{_data.Customers.CustomerList.Count} Kunden geladen.";
        } catch (Exception ex) { StatusText.Text = $"Laden fehlgeschlagen: {ex.Message}"; }
    }
    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        DataContext = CustomersList.SelectedItem as Customer;
    }
    private void Save_Click(object sender, RoutedEventArgs e) {
        try {
            _data?.Save(_path);
            StatusText.Text = "Lokale Ausgabekopie gespeichert.";
        } catch (Exception ex) { StatusText.Text = $"Speichern fehlgeschlagen: {ex.Message}"; }
    }
    private void Rename_Click(object sender, RoutedEventArgs e) {
        if (DataContext is Customer c)
            c.CompanyName += " *";
    }
    private void Add_Click(object sender, RoutedEventArgs e) {
        if (_data is null)
            return;
        var c = new Customer { CustomerID = Guid.NewGuid().ToString("N")[..8], CompanyName = "Neue Firma", ContactName = "", Phone = "+41" };
        _data.Customers.CustomerList.Add(c);
        CustomersList.SelectedItem = c;
    }
    private void Remove_Click(object sender, RoutedEventArgs e) {
        if (_data is null || CustomersList.SelectedItem is not Customer c)
            return;
        _data.Customers.CustomerList.Remove(c);
        CustomersList.SelectedIndex = _data.Customers.CustomerList.Count > 0 ? 0 : -1;
        DataContext = CustomersList.SelectedItem as Customer;
    }
}
