namespace Course;

public sealed class MessageService {
    private readonly TextWriter output;

    public MessageService(TextWriter output) {
        this.output = output;
    }

    public void SendMessage(ICryptoAlgorithm cipher, string message) {
        ArgumentNullException.ThrowIfNull(cipher);
        // TODO: Über das Interface verschlüsseln und auf output schreiben.
        output.WriteLine(message);
    }
}
