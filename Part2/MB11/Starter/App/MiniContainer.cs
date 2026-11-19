namespace Course.Container;

// Lernmodell: genau ein public Konstruktor, nur Transient.
// Es erkennt Zyklen, kann sie aber nicht auflösen.
public sealed class MiniContainer {
    private readonly Dictionary<Type, Type> registrations = new();

    public void Register<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService {
        var implementation = typeof(TImplementation);
        if (implementation.IsAbstract)
            throw new ArgumentException("Die Implementierung muss konkret sein.");
        // Erneutes Register ersetzt die bisherige Zuordnung.
        registrations[typeof(TService)] = implementation;
    }

    public T Resolve<T>() where T : class =>
        (T)Resolve(typeof(T), new List<Type>());

    private object Resolve(Type service, List<Type> path) {
        if (path.Contains(service)) {
            var cycle = string.Join(" -> ", path.Append(service).Select(t => t.Name));
            throw new InvalidOperationException($"Zyklus: {cycle}");
        }
        if (!registrations.TryGetValue(service, out var implementation))
            throw new InvalidOperationException($"Registrierung fehlt: {service.Name}");

        var constructors = implementation.GetConstructors();
        if (constructors.Length != 1)
            throw new InvalidOperationException(
                $"{implementation.Name} braucht genau einen public Konstruktor.");

        path.Add(service);
        try {
            var constructor = constructors[0];
            var arguments = constructor.GetParameters()
                .Select(parameter => Resolve(parameter.ParameterType, path))
                .ToArray();
            return constructor.Invoke(arguments);
        } finally {
            // Nur der aktuelle Aufrufpfad ist aktiv, nicht alle jemals erstellten Typen.
            path.RemoveAt(path.Count - 1);
        }
    }
}

public sealed class CycleA(CycleB other) {
    public CycleB Other { get; } = other;
}

public sealed class CycleB(CycleA other) {
    public CycleA Other { get; } = other;
}
