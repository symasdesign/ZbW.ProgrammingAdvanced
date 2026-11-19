namespace Course;

public sealed class MessageService {
    private readonly TextWriter output;

    public MessageService(TextWriter output) {
        this.output = output;
    }

    public void SendMessage(ICryptoAlgorithm cipher, string message) {
        ArgumentNullException.ThrowIfNull(cipher);
        output.WriteLine(cipher.Encrypt(message));
    }
}
