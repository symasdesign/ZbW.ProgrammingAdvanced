namespace Course;

// Vorbereitete Ergebniskontrolle; kein zusätzlicher Unterrichtsgegenstand.
public static class SelfTests {
    public static int Run() {
        Check.Run("Caesar Ergebnis", () =>
            Check.Equal("Def Abc!", new CaesarCipher(3).Encrypt("Abc Xyz!")));
        Check.Run("Caesar Roundtrip", () => {
            ICryptoAlgorithm cipher = new CaesarCipher(3);
            Check.Equal("ZbW 2026!", cipher.Decrypt(cipher.Encrypt("ZbW 2026!")));
        });
        Check.Run("Negativer Schlüssel", () =>
            Check.Equal("Zab", new CaesarCipher(-1).Encrypt("Abc")));
        Check.Run("Umlaute unverändert", () =>
            Check.Equal("Äöü 123!", new CaesarCipher(3).Encrypt("Äöü 123!")));
        Check.Run("Leerer Text", () =>
            Check.Equal("", new CaesarCipher(7).Encrypt("")));
        Check.Run("Vigenère Ergebnis", () =>
            Check.Equal("LXFOPVEFRNHR", new ViginereCipher("LEMON").Encrypt("ATTACKATDAWN")));
        Check.Run("Vigenère Roundtrip", () => {
            ICryptoAlgorithm cipher = new ViginereCipher("KEY");
            Check.Equal("Hallo ZbW!", cipher.Decrypt(cipher.Encrypt("Hallo ZbW!")));
        });
        Check.Run("Override über Basistyp", () => {
            CryptoAlgorithm cipher = new ViginereCipher("LEMON");
            Check.Equal(5, cipher.KeyStrength);
        });
        Check.Run("Dienst mit Caesar", () => {
            var output = new StringWriter();
            new MessageService(output).SendMessage(new CaesarCipher(3), "Abc");
            Check.Equal("Def" + Environment.NewLine, output.ToString());
        });
        Check.Run("Dienst mit Vigenère", () => {
            var output = new StringWriter();
            new MessageService(output).SendMessage(new ViginereCipher("KEY"), "ABC");
            Check.Equal("KFA" + Environment.NewLine, output.ToString());
        });
        Check.Run("Null-Eingabe", () =>
            Check.Throws<ArgumentNullException>(() => new CaesarCipher(1).Encrypt(null!)));
        return Check.Finish();
    }
}
