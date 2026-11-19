namespace Course;

public sealed class Address {
    public int PostalCode {
        get; set;
    }
    public string City {
        get; set;
    }
    public Address(int postalCode, string city) {
        PostalCode = postalCode;
        City = city;
    }
}
public sealed class Person {
    public string Name {
        get; set;
    }
    public int Age {
        get; set;
    }
    public Address Address {
        get; set;
    }
    public Person(string name, int age, Address address) {
        Name = name;
        Age = age;
        Address = address;
    }
    public Person ShallowCopy() => (Person)MemberwiseClone();
    public Person DeepCopy() =>
        ShallowCopy(); // TODO: Auch die veränderliche Adresse kopieren.
}
