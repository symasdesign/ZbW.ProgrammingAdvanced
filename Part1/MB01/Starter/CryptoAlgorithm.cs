namespace Course;

// Bestandsbeispiel zur Softwarestruktur, keine sichere Verschlüsselung.
public interface ICryptoAlgorithm {
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}

public abstract class CryptoAlgorithm : ICryptoAlgorithm {
    public string Name { get; protected set; } = "";
    public string Description { get; protected set; } = "";
    public bool IsSymmetric { get; protected set; }
    public virtual int KeyStrength => 1;
    public abstract string Encrypt(string plainText);
    public abstract string Decrypt(string cipherText);
}
