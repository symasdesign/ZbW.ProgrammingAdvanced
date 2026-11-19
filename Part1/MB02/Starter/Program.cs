using Course;

internal class Program {
    private static int Main(string[] args) {
        if (args.Contains("--self-test"))
            return SelfTests.Run();
        var a = new PhoneNumber("123", "456", "7890");
        var b = new PhoneNumber("123", "456", "7890");
        Console.WriteLine($"Referenzgleich: {ReferenceEquals(a, b)}");
        Console.WriteLine($"Wertgleich: {a.Equals(b)}");
        var person = new Person("Alex", 30, new Address(9000, "St. Gallen"));
        var shallow = person.ShallowCopy();
        shallow.Address.City = "Bern";
        Console.WriteLine($"Original nach flacher Kopie: {person.Address.City}");
        Console.WriteLine("Tests: dotnet run -- --self-test");
        return 0;
    }
}
