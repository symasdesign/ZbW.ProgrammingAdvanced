namespace Course;

public sealed class StatisticsSubscriber {
    private readonly Logger logger;
    private bool attached;
    private readonly Dictionary<MessageLevel, int> counts = new() {
        [MessageLevel.Debug] = 0,
        [MessageLevel.Info] = 0,
        [MessageLevel.Error] = 0
    };

    public StatisticsSubscriber(Logger logger) => this.logger = logger;
    public int Count(MessageLevel level) => counts[level];

    public void Attach() {
        // TODO: Doppelte Anmeldung verhindern.
        _ = attached;
        _ = logger;
        attached = false;
        throw new NotImplementedException("Attach implementieren.");
    }

    public void Detach() {
        // TODO: Nur bei aktiver Anmeldung entfernen.
        throw new NotImplementedException("Detach implementieren.");
    }

    private void OnMessage(object? sender, LogMessageEventArgs data) {
        // TODO: Den Zähler des empfangenen Levels erhöhen.
        throw new NotImplementedException("OnMessage implementieren.");
    }
}
