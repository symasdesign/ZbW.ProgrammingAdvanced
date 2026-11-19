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
        if (attached)
            return;
        logger.MessageLogged += OnMessage;
        attached = true;
    }

    public void Detach() {
        if (!attached)
            return;
        logger.MessageLogged -= OnMessage;
        attached = false;
    }

    private void OnMessage(object? sender, LogMessageEventArgs data) {
        counts[data.Level]++;
    }
}
