namespace Course;

public static class SelfTests {
    public static int Run(string group) {
        if (group is not ("all" or "core" or "independent" or "homework")) {
            Console.WriteLine("Gruppen: core, independent, homework, all");
            return 2;
        }
        var c = new Checks();
        if (group is "all" or "core") {
            c.Test("Fallback ohne Subscriber", () => {
                using var output = new StringWriter();
                var logger = new Logger(output);
                logger.LogMessage(MessageLevel.Info, "Start");
                Checks.Equal("INFO: Start" + Environment.NewLine, output.ToString());
            });
            c.Test("Daten und Sender", () => {
                using var output = new StringWriter();
                var logger = new Logger(output);
                object? sender = null;
                LogMessageEventArgs? data = null;
                logger.MessageLogged += (s, e) => { sender = s; data = e; };
                logger.LogMessage(MessageLevel.Error, "Fehler");
                Checks.Equal(true, ReferenceEquals(logger, sender));
                Checks.Equal(MessageLevel.Error, data!.Level);
                Checks.Equal("Fehler", data.Message);
                Checks.Equal("", output.ToString());
            });
            c.Test("Mehrere Subscriber in Anmeldereihenfolge", () => {
                var logger = new Logger();
                var calls = new List<string>();
                logger.MessageLogged += (_, _) => calls.Add("A");
                logger.MessageLogged += (_, _) => calls.Add("B");
                logger.LogMessage(MessageLevel.Info, "x");
                Checks.Sequence(new[] { "A", "B" }, calls);
            });
            c.Test("Abmeldung", () => {
                using var output = new StringWriter();
                var logger = new Logger(output);
                var count = 0;
                EventHandler<LogMessageEventArgs> handler = (_, _) => count++;
                logger.MessageLogged += handler;
                logger.LogMessage(MessageLevel.Info, "eins");
                logger.MessageLogged -= handler;
                logger.LogMessage(MessageLevel.Info, "zwei");
                Checks.Equal(1, count);
                Checks.Equal("INFO: zwei" + Environment.NewLine, output.ToString());
            });
            c.Test("Null-Nachricht", () => Checks.Throws<ArgumentNullException>(() =>
                new Logger().LogMessage(MessageLevel.Info, null!)));
        }
        if (group is "all" or "independent") {
            c.Test("Drei Level an richtige Ausgabekanäle", () => {
                using var output = new StringWriter();
                using var error = new StringWriter();
                var logger = new Logger(output);
                var s = new ConsoleSubscribers(output, error);
                logger.MessageLogged += s.Debug;
                logger.MessageLogged += s.Info;
                logger.MessageLogged += s.Error;
                logger.LogMessage(MessageLevel.Debug, "D");
                logger.LogMessage(MessageLevel.Info, "I");
                logger.LogMessage(MessageLevel.Error, "E");
                Checks.Equal("DEBUG: D" + Environment.NewLine + "INFO: I" + Environment.NewLine,
                    output.ToString());
                Checks.Equal("ERROR: E" + Environment.NewLine, error.ToString());
            });
            c.Test("Kein Fallback bei vorhandenem unpassendem Subscriber", () => {
                using var output = new StringWriter();
                using var error = new StringWriter();
                var logger = new Logger(output);
                var s = new ConsoleSubscribers(output, error);
                logger.MessageLogged += s.Info;
                logger.LogMessage(MessageLevel.Debug, "nicht ausgegeben");
                Checks.Equal("", output.ToString());
            });
            c.Test("Doppelte Anmeldung ist doppelte Benachrichtigung", () => {
                var logger = new Logger();
                var count = 0;
                EventHandler<LogMessageEventArgs> h = (_, _) => count++;
                logger.MessageLogged += h;
                logger.MessageLogged += h;
                logger.LogMessage(MessageLevel.Info, "eins");
                logger.MessageLogged -= h;
                logger.LogMessage(MessageLevel.Info, "zwei");
                Checks.Equal(3, count);
            });
            c.Test("Exception stoppt nachfolgende Handler", () => {
                var logger = new Logger();
                var secondCalled = false;
                logger.MessageLogged += (_, _) => throw new InvalidOperationException("Testfehler");
                logger.MessageLogged += (_, _) => secondCalled = true;
                Checks.Throws<InvalidOperationException>(() => logger.LogMessage(MessageLevel.Info, "x"));
                Checks.Equal(false, secondCalled);
            });
        }
        if (group is "all" or "homework") {
            c.Test("Statistik zählt je Level", () => {
                using var output = new StringWriter();
                var logger = new Logger(output);
                var s = new StatisticsSubscriber(logger);
                s.Attach();
                logger.LogMessage(MessageLevel.Info, "a");
                logger.LogMessage(MessageLevel.Info, "b");
                logger.LogMessage(MessageLevel.Error, "c");
                Checks.Equal(2, s.Count(MessageLevel.Info));
                Checks.Equal(1, s.Count(MessageLevel.Error));
                Checks.Equal(0, s.Count(MessageLevel.Debug));
            });
            c.Test("Attach ist idempotent", () => {
                var logger = new Logger();
                var s = new StatisticsSubscriber(logger);
                s.Attach();
                s.Attach();
                logger.LogMessage(MessageLevel.Info, "x");
                Checks.Equal(1, s.Count(MessageLevel.Info));
            });
            c.Test("Detach stoppt Statistik", () => {
                using var output = new StringWriter();
                var logger = new Logger(output);
                var s = new StatisticsSubscriber(logger);
                s.Attach();
                logger.LogMessage(MessageLevel.Info, "a");
                s.Detach();
                s.Detach();
                logger.LogMessage(MessageLevel.Info, "b");
                Checks.Equal(1, s.Count(MessageLevel.Info));
                Checks.Equal("INFO: b" + Environment.NewLine, output.ToString());
            });
            c.Test("Wiederanmeldung behält Werte", () => {
                var logger = new Logger();
                var s = new StatisticsSubscriber(logger);
                s.Attach();
                logger.LogMessage(MessageLevel.Error, "a");
                s.Detach();
                s.Attach();
                logger.LogMessage(MessageLevel.Error, "b");
                Checks.Equal(2, s.Count(MessageLevel.Error));
            });
        }
        return c.Finish();
    }
}
