namespace CleanCodeDemo;

public sealed class Customer {
    public Customer(int id, string loginName, string lastName, int age) {
        Id = id;
        LoginName = loginName;
        LastName = lastName;
        Age = age;
    }

    public int Id {
        get;
    }
    public string LoginName {
        get;
    }
    public string LastName {
        get;
    }
    public int Age {
        get;
    }

    public override string ToString() => $"{LoginName}: {Age} ({LastName})";
}
