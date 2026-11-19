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

}
