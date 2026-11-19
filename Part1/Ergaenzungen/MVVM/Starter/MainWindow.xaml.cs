using WpfExercise.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfExercise {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private Datasource.Data Data {
            get; set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.Data = Datasource.Data.Load(CustomersRepository.DataPath);
            this.listbox.ItemsSource = this.Data.Customers.CustomerList;
            this.listbox.SelectedItem = this.Data.Customers.CustomerList.FirstOrDefault();
        }

        private void Close_Click(object sender, RoutedEventArgs e) {
            this.Data.Save(CustomersRepository.DataPath);
            this.Close();
        }


        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lb = (ListBox)sender;
            this.DataContext = lb.SelectedItem;
        }
    }
}
