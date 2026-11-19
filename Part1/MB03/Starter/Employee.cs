namespace Course;

public sealed class Employee {
    public int Id {
        get;
    }
    public string Name {
        get;
    }
    public int Age {
        get;
    }
    public decimal Salary {
        get;
    }

    public Employee(int id, string name, int age, decimal salary) {
        Id = id;
        Name = name;
        Age = age;
        Salary = salary;
    }
}

public static class EmployeeFilters {
    public static List<Employee> Filter(List<Employee> source, Func<Employee, bool> predicate) {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        // TODO: Filterung ohne Änderung der Eingabeliste.
        throw new NotImplementedException("Mitarbeiterfilter implementieren.");
    }
}
