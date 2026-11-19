using System.Net;

internal class Program {
    private static async Task Main(string[] args) {
        var path = args.Length > 0 ? args[0] : "weatherforecast?days=2";

        // Ein Client für den gesamten kleinen Programmlauf.
        using var client = new HttpClient {
            BaseAddress = new Uri("http://localhost:5052/")
        };

        // Abbruch nach fünf Sekunden oder durch Ctrl+C.
        using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        Console.CancelKeyPress += (_, eventArgs) => {
            eventArgs.Cancel = true;
            cancellation.Cancel();
        };

        try {
            Console.WriteLine($"GET {client.BaseAddress}{path}");

            using var response = await client.GetAsync(
                path,
                HttpCompletionOption.ResponseHeadersRead,
                cancellation.Token);

            var body = await response.Content.ReadAsStringAsync(cancellation.Token);

            Console.WriteLine($"Status: {(int)response.StatusCode} {response.StatusCode}");
            Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");

            switch (response.StatusCode) {
                case HttpStatusCode.OK:
                    Console.WriteLine("Die API liefert Wetter-Demodaten als JSON.");
                    break;
                case HttpStatusCode.BadRequest:
                    Console.WriteLine("Die API hat die Eingabe abgelehnt.");
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Für diesen Pfad gibt es keinen Endpunkt.");
                    break;
                default:
                    Console.WriteLine("Unerwarteter Statuscode.");
                    break;
            }

            if (!string.IsNullOrWhiteSpace(body)) {
                Console.WriteLine(body);
            }

            if (!response.IsSuccessStatusCode) {
                Environment.ExitCode = 1;
            }
        } catch (OperationCanceledException) when (cancellation.IsCancellationRequested) {
            Console.WriteLine("Anfrage abgebrochen: Zeitlimit erreicht oder Ctrl+C gedrückt.");
            Environment.ExitCode = 1;
        } catch (HttpRequestException exception) {
            Console.WriteLine($"Die Anfrage konnte nicht abgeschlossen werden: {exception.Message}");
            Environment.ExitCode = 1;
        }
    }
}
