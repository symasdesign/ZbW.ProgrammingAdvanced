using System.Globalization;
using System.Text;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "interfaces" or "delegates" or "sequence")) {
            Console.Error.WriteLine("Modus: all | interfaces | delegates | sequence.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        Run("interfaces", Activities.Interfaces);
        Run("delegates", Activities.Delegates);
        Run("sequence", Activities.ReadOnlySequence);
    }
}

public abstract record Animal(string Name);
public sealed record Dog(string Name) : Animal(Name);
public sealed record Cat(string Name) : Animal(Name);
public interface IAnimalFactory<out T> {
    T Create();
}
public interface IAnimalFeeder<in T> {
    string Feed(T animal);
}
public interface IAnimalPairer<T> {
    T Mate(T partner);
}
public sealed class DogFactory : IAnimalFactory<Dog> {
    public Dog Create() => new("Rex");
}
public sealed class UniversalFeeder : IAnimalFeeder<Animal> {
    public string Feed(Animal animal) => $"Futter für {animal.Name}";
}
public static class Activities {
    public static void Interfaces() {
        IAnimalFactory<Animal> factory = new DogFactory();
        var animal = factory.Create();
        Console.WriteLine($"Factory liefert {animal.Name}");
        IAnimalFeeder<Dog> feeder = new UniversalFeeder();
        Console.WriteLine(feeder.Feed(new Dog("Rex")));
    }
    public static void Delegates() {
        Action<Animal> logAnimal = a => Console.WriteLine($"Log: {a.Name}");
        Action<Dog> logDog = logAnimal;
        logDog(new Dog("Rex"));
        Func<Dog> makeDog = () => new Dog("Luna");
        Func<Animal> makeAnimal = makeDog;
        Console.WriteLine($"Delegate liefert {makeAnimal().Name}");
    }
    public static void ReadOnlySequence() {
        var dogs = new List<Dog> { new("Rex"), new("Luna") };
        IEnumerable<Animal> animals = dogs;
        foreach (var animal in animals)
            Console.WriteLine(animal.Name);
        // List<Animal> all = dogs; ist nicht erlaubt.
        // all.Add(new Cat("Mimi")); dürfte keine Katzen in dogs einfügen.
    }
}
