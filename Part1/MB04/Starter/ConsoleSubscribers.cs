namespace Course;

public sealed class ConsoleSubscribers {
    private readonly TextWriter output;
    private readonly TextWriter error;

    public ConsoleSubscribers(TextWriter output, TextWriter error) {
        this.output = output;
        this.error = error;
    }

    public void Debug(object? sender, LogMessageEventArgs data) {
        // TODO: Nur Level Debug auf output ausgeben.
        throw new NotImplementedException("Subscriber implementieren.");
    }

    public void Info(object? sender, LogMessageEventArgs data) {
        // TODO: Nur Level Info auf output ausgeben.
        throw new NotImplementedException("Subscriber implementieren.");
    }

    public void Error(object? sender, LogMessageEventArgs data) {
        // TODO: Nur Level Error auf error ausgeben.
        throw new NotImplementedException("Subscriber implementieren.");
    }
}
