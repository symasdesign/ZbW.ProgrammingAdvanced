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
            try {
                action();
            } catch (NotImplementedException) {
                Console.WriteLine("TODO: Aufgabenmethode vervollständigen.");
            }
        }

        Run("interfaces", Activities.Interfaces);
        Run("delegates", Activities.Delegates);
        Run("sequence", Activities.ReadOnlySequence);
    }
}

public abstract record Animal(string Name);
public sealed record Dog(string Name) : Animal(Name);
public sealed record Cat(string Name) : Animal(Name);
public interface IAnimalFactory<T> {
    T Create();
}
public interface IAnimalFeeder<T> {
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
        // TODO: out/in ergänzen und sichere Zuordnungen implementieren.
        throw new NotImplementedException();
    }
    public static void Delegates() {
        // TODO: Func/Action in zulässiger Richtung zuordnen.
        throw new NotImplementedException();
    }
    public static void ReadOnlySequence() {
        // TODO: IEnumerable<Animal> benutzen; List-Invarianz erklären.
        throw new NotImplementedException();
    }
}
