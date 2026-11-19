using Course;

internal class Program {
    private static int Main(string[] args) {
        if (args.Length > 0 && args[0] == "--self-test")
            return SelfTests.Run(args.Length > 1 ? args[1] : "all");

        var logger = new Logger();
        var subscribers = new ConsoleSubscribers(Console.Out, Console.Error);
        try {
            logger.LogMessage(MessageLevel.Info, "Ohne Subscriber: Fallback.");
            logger.MessageLogged += subscribers.Debug;
            logger.MessageLogged += subscribers.Info;
            logger.MessageLogged += subscribers.Error;
            logger.LogMessage(MessageLevel.Debug, "Diagnose aktiv.");
            logger.LogMessage(MessageLevel.Info, "Programm gestartet.");
            logger.LogMessage(MessageLevel.Error, "Datei nicht gefunden.");
        } catch (NotImplementedException ex) {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Öffnen Sie Logger.cs; prüfen mit --self-test core.");
        } finally {
            logger.MessageLogged -= subscribers.Debug;
            logger.MessageLogged -= subscribers.Info;
            logger.MessageLogged -= subscribers.Error;
        }
        return 0;
    }
}
