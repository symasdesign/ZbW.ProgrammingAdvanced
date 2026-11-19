using System.Text;

namespace Course;

// Name und Ablauf aus dem bisherigen Kurs beibehalten.
public class ViginereCipher : CryptoAlgorithm {
    private readonly string key;
    public override int KeyStrength => key.Length;

    public ViginereCipher(string key) {
        ArgumentException.ThrowIfNullOrEmpty(key);
        if (key.Any(c => !char.IsAsciiLetter(c))) {
            throw new ArgumentException("Der Schlüssel enthält nur A-Z/a-z.", nameof(key));
        }
        this.key = key.ToUpperInvariant();
        Name = "Vigenère Cipher";
        Description = "Caesar-Verschiebung mit wiederholtem Schlüssel";
        IsSymmetric = true;
    }

    public override string Encrypt(string plainText) => ProcessText(plainText, true);
    public override string Decrypt(string cipherText) => ProcessText(cipherText, false);

    private string ProcessText(string text, bool encrypt) {
        ArgumentNullException.ThrowIfNull(text);
        var result = new StringBuilder();
        var keyIndex = 0;
        foreach (var c in text) {
            if (char.IsAsciiLetter(c)) {
                var baseChar = char.IsUpper(c) ? 'A' : 'a';
                var shift = key[keyIndex % key.Length] - 'A';
                if (!encrypt) {
                    shift = 26 - shift;
                }
                result.Append((char)(baseChar + (c - baseChar + shift) % 26));
                keyIndex++;
            } else {
                result.Append(c);
            }
        }
        return result.ToString();
    }
}
