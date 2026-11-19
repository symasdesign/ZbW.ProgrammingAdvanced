#nullable disable
// Aus bestehender Datasource; .NET 10, lokale Dateipfade und ohne IDE-Annotations.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;


namespace Datasource {
    [XmlRoot("Root")]
    public class Data {
        public static Data Load(string path = "CustomersOrders.xml") {
            var serializer = new XmlSerializer(typeof(Data));
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                return (Data)serializer.Deserialize(fileStream);
            }
        }

        public void Save(string path = "CustomersOrders.xml") {
            var serializer = new XmlSerializer(typeof(Data));
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                serializer.Serialize(fileStream, this);
            }
        }

        [XmlElement("Customers")]
        public Customers Customers {
            get; set;
        }
        [XmlElement("Orders")]
        public Orders Orders {
            get; set;
        }
    }

    public class Customers {
        [XmlElement("Customer")]
        public ObservableCollection<Customer> CustomerList {
            get; set;
        }
    }

    public class Customer : IDataErrorInfo, INotifyPropertyChanged {
        private string _customerId;
        private string _companyName;
        private string _contactName;
        private string _contactTitle;
        private string _phone;
        private string _fax;
        private DateTime? _birthday;
        private FullAddress _fullAddress;

        [XmlAttribute("CustomerID")]
        public string CustomerID {
            get => this._customerId;
            set => SetField(ref _customerId, value);
        }

        [XmlElement("CompanyName")]
        public string CompanyName {
            get => this._companyName;
            set => SetField(ref _companyName, value);
        }

        [XmlElement("ContactName")]
        public string ContactName {
            get => this._contactName;
            set => SetField(ref _contactName, value);
        }

        [XmlElement("ContactTitle")]
        public string ContactTitle {
            get => this._contactTitle;
            set => SetField(ref _contactTitle, value);
        }

        [XmlElement("Phone")]
        public string Phone {
            get => this._phone;
            set => SetField(ref _phone, value);
        }

        [XmlElement("Fax")]
        public string Fax {
            get => this._fax;
            set => SetField(ref _fax, value);
        }

        [XmlElement("Birthday")]
        public DateTime? Birthday {
            get => this._birthday;
            set => SetField(ref _birthday, value);
        }

        [XmlElement("FullAddress")]
        public FullAddress FullAddress {
            get => this._fullAddress;
            set => SetField(ref _fullAddress, value);
        }

        public override string ToString() {
            return $"CustomerID: {this.CustomerID}, " +
                   $"CompanyName: {this.CompanyName}, " +
                   $"ContactName: {this.ContactName}";
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Error für Objekt
        public string Error => "";

        // Error für bestimmtes Property
        public string this[string columnName] {
            get {
                switch (columnName) {
                    case "CustomerID":
                        if (string.IsNullOrEmpty(this.CustomerID)) {
                            return "CustomerID cannot be empty.";
                        }
                        break;
                    case "CompanyName":
                        if (string.IsNullOrEmpty(this.CompanyName)) {
                            return "CompanyName cannot be empty.";
                        }
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(this.Phone)) {
                            return "Phone cannot be empty.";
                        }
                        break;
                    case "ContactName":
                    case "Birthday":
                        if (!string.IsNullOrEmpty(this.ContactName) && !this.Birthday.HasValue) {
                            return "Contactname must also specify a Birthday.";
                        }
                        if (string.IsNullOrEmpty(this.ContactName) && this.Birthday.HasValue) {
                            return "Birthday must also specify a Contactname.";
                        }
                        break;


                    case "Name":
                        break;


                    default:
                        throw new ArgumentException("Unknown property: " + columnName, nameof(columnName));
                }

                return "";
            }
        }

        public override bool Equals(object obj) {
            if ((obj as Customer) == null) {
                return false;
            }

            return string.Equals(this.CustomerID, ((Customer)obj).CustomerID);
        }

        public override int GetHashCode() {
            var hashCode = 406553661;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.CustomerID);
            return hashCode;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FullAddress : INotifyPropertyChanged {
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;

        [XmlElement("Address")]
        public string Address {
            get => this._address;
            set => SetField(ref _address, value);
        }

        [XmlElement("City")]
        public string City {
            get => this._city;
            set => SetField(ref _city, value);
        }

        [XmlElement("Region")]
        public string Region {
            get => this._region;
            set => SetField(ref _region, value);
        }

        [XmlElement("PostalCode")]
        public string PostalCode {
            get => this._postalCode;
            set => SetField(ref _postalCode, value);
        }

        [XmlElement("Country")]
        public string Country {
            get => this._country;
            set => SetField(ref _country, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class Orders {
        [XmlElement("Order")]
        public ObservableCollection<Order> OrderList {
            get; set;
        }
    }

    public class Order : INotifyPropertyChanged {
        private string _customerId;
        private int _employeeId;
        private DateTime _orderDate;
        private DateTime _requiredDate;
        private ShipInfo _shipInfo;

        [XmlElement("CustomerID")]
        public string CustomerID {
            get => this._customerId;
            set => SetField(ref _customerId, value);
        }

        [XmlElement("EmployeeID")]
        public int EmployeeID {
            get => this._employeeId;
            set => SetField(ref _employeeId, value);
        }

        [XmlElement("OrderDate")]
        public DateTime OrderDate {
            get => this._orderDate;
            set => SetField(ref _orderDate, value);
        }

        [XmlElement("RequiredDate")]
        public DateTime RequiredDate {
            get => this._requiredDate;
            set => SetField(ref _requiredDate, value);
        }

        [XmlElement("ShipInfo")]
        public ShipInfo ShipInfo {
            get => this._shipInfo;
            set => SetField(ref _shipInfo, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class ShipInfo : INotifyPropertyChanged {
        private string _shipCountry;
        private string _shipPostalCode;
        private string _shipRegion;
        private string _shipCity;
        private string _shipAddress;
        private string _shipName;
        private decimal _freight;
        private int _shipVia;
        private DateTime _shippedDate;

        [XmlAttribute("ShippedDate")]
        public DateTime ShippedDate {
            get => this._shippedDate;
            set => SetField(ref _shippedDate, value);
        }

        [XmlElement("ShipVia")]
        public int ShipVia {
            get => this._shipVia;
            set => SetField(ref _shipVia, value);
        }

        [XmlElement("Freight")]
        public decimal Freight {
            get => this._freight;
            set => SetField(ref _freight, value);
        }

        [XmlElement("ShipName")]
        public string ShipName {
            get => this._shipName;
            set => SetField(ref _shipName, value);
        }

        [XmlElement("ShipAddress")]
        public string ShipAddress {
            get => this._shipAddress;
            set => SetField(ref _shipAddress, value);
        }

        [XmlElement("ShipCity")]
        public string ShipCity {
            get => this._shipCity;
            set => SetField(ref _shipCity, value);
        }

        [XmlElement("ShipRegion")]
        public string ShipRegion {
            get => this._shipRegion;
            set => SetField(ref _shipRegion, value);
        }

        [XmlElement("ShipPostalCode")]
        public string ShipPostalCode {
            get => this._shipPostalCode;
            set => SetField(ref _shipPostalCode, value);
        }

        [XmlElement("ShipCountry")]
        public string ShipCountry {
            get => this._shipCountry;
            set => SetField(ref _shipCountry, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
