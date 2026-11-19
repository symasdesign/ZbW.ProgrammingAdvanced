namespace Course;

public enum MessageLevel {
    Debug, Info, Error
}

public sealed class LogMessageEventArgs : EventArgs {
    public MessageLevel Level {
        get;
    }
    public string Message {
        get;
    }

    public LogMessageEventArgs(MessageLevel level, string message) {
        ArgumentNullException.ThrowIfNull(message);
        Level = level;
        Message = message;
    }
}

public sealed class Logger {
    private readonly TextWriter fallback;
    public event EventHandler<LogMessageEventArgs>? MessageLogged;

    // Ein austauschbarer Ausgabekanal macht das Verhalten ohne echte Konsole prüfbar.
    public Logger(TextWriter? fallback = null) => this.fallback = fallback ?? Console.Out;

    public static string Format(LogMessageEventArgs data) =>
        $"{data.Level.ToString().ToUpperInvariant()}: {data.Message}";

    public void LogMessage(MessageLevel level, string message) {
        ArgumentNullException.ThrowIfNull(message);
        var data = new LogMessageEventArgs(level, message);
        var handlers = MessageLogged;
        // TODO: Ohne Handler auf fallback schreiben, sonst genau einmal auslösen.
        _ = handlers;
        _ = data;
        throw new NotImplementedException("LogMessage implementieren.");
    }
}
