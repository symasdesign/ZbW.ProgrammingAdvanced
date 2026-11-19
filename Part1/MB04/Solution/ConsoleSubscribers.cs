namespace Course;

public sealed class ConsoleSubscribers {
    private readonly TextWriter output;
    private readonly TextWriter error;

    public ConsoleSubscribers(TextWriter output, TextWriter error) {
        this.output = output;
        this.error = error;
    }

    public void Debug(object? sender, LogMessageEventArgs data) {
        if (data.Level == MessageLevel.Debug)
            output.WriteLine(Logger.Format(data));
    }

    public void Info(object? sender, LogMessageEventArgs data) {
        if (data.Level == MessageLevel.Info)
            output.WriteLine(Logger.Format(data));
    }

    public void Error(object? sender, LogMessageEventArgs data) {
        if (data.Level == MessageLevel.Error)
            error.WriteLine(Logger.Format(data));
    }
}
