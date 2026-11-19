using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Datasource;
using WpfExercise.Services;

namespace WpfExercise.Customers;

public sealed class CustomersViewModel : INotifyPropertyChanged {
    private readonly ICustomersRepository repository = new CustomersRepository();
    private Customer currentCustomer;
    private bool saving;

    public ObservableCollection<Customer> Customers { get; } = [];
    public RelayCommand SaveCommand { get; }
    public event PropertyChangedEventHandler PropertyChanged;

    public CustomersViewModel() {
        SaveCommand = new RelayCommand(OnSave, () => CurrentCustomer != null && !saving);
    }

    public Customer CurrentCustomer {
        get => currentCustomer;
        set {
            if (ReferenceEquals(currentCustomer, value)) {
                return;
            }
            currentCustomer = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCustomer)));
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public async void Load() {
        try {
            var customers = await repository.GetCustomersAsync();
            Customers.Clear();
            foreach (var customer in customers) {
                Customers.Add(customer);
            }
            CurrentCustomer = Customers.FirstOrDefault();
        } catch (Exception ex) {
            MessageBox.Show(ex.Message, "Kunden laden");
        }
    }

    private async void OnSave() {
        saving = true;
        SaveCommand.RaiseCanExecuteChanged();
        try {
            await repository.UpdateCustomerAsync(CurrentCustomer);
        } catch (Exception ex) {
            MessageBox.Show(ex.Message, "Kunde speichern");
        } finally {
            saving = false;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}
