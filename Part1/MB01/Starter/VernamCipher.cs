namespace Course;

// Didaktische Variante der Bestandsaufgabe: Vigenère mit gleich langem Schlüssel.
// Dies ist kein kryptografisch sicheres One-Time-Pad.
public sealed class VernamCipher : ViginereCipher {
    public VernamCipher(string key) : base(key) {
        Name = "Vernam Lernvariante";
    }

    public override string Encrypt(string plainText) {
        ValidateLength(plainText);
        return base.Encrypt(plainText);
    }

    public override string Decrypt(string cipherText) {
        ValidateLength(cipherText);
        return base.Decrypt(cipherText);
    }

    private void ValidateLength(string text) {
        ArgumentNullException.ThrowIfNull(text);
        // TODO: Genau gleiche Länge von Schlüssel und Text verlangen.
        throw new NotImplementedException("Längenprüfung ergänzen.");
    }
}
