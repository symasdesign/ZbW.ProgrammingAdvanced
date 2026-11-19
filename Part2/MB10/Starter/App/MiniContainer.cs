namespace Course.Container;

public sealed class MiniContainer {
    private readonly Dictionary<Type, Type> registrations = new();

    public void Register<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService {
        // TODO: eine Typzuordnung speichern, keine Instanz erzeugen.
        throw new NotImplementedException("Register implementieren.");
    }

    public T Resolve<T>() where T : class =>
        (T)Resolve(typeof(T), new List<Type>());

    private object Resolve(Type service, List<Type> path) {
        // TODO: Zuordnung und genau einen public Konstruktor bestimmen.
        // TODO: Parameter rekursiv auflösen und ConstructorInfo.Invoke aufrufen.
        // TODO: fehlende Registrierung und aktiven Zyklus unterscheiden.
        // path vor Rekursion ergaenzen und im finally wieder entfernen.
        throw new NotImplementedException("Resolve implementieren.");
    }
}

public sealed class CycleA(CycleB other) {
    public CycleB Other { get; } = other;
}

public sealed class CycleB(CycleA other) {
    public CycleA Other { get; } = other;
}
