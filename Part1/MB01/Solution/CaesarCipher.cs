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
        var chars = text.ToCharArray();
        for (var i = 0; i < chars.Length; i++) {
            var c = chars[i];
            char start;
            if (c is >= 'A' and <= 'Z') {
                start = 'A';
            } else if (c is >= 'a' and <= 'z') {
                start = 'a';
            } else {
                continue;
            }
            chars[i] = (char)(start + (c - start + offset + 26) % 26);
        }
        return new string(chars);
    }
}
