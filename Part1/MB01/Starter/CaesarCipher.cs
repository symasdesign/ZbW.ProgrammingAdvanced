namespace Course;

public sealed class CaesarCipher : CryptoAlgorithm {
    private readonly int shift;

    public CaesarCipher(int shift) {
        this.shift = ((shift % 26) + 26) % 26;
        Name = "Caesar Cipher";
        Description = "ASCII-Buchstaben verschieben";
        IsSymmetric = true;
    }

    public override string Encrypt(string plainText) => Shift(plainText, shift);
    public override string Decrypt(string cipherText) => Shift(cipherText, -shift);

    private static string Shift(string text, int offset) {
        ArgumentNullException.ThrowIfNull(text);
        // TODO: A-Z/a-z zyklisch verschieben; alle anderen Zeichen beibehalten.
        throw new NotImplementedException("Caesar-Verschiebung implementieren.");
    }
}
