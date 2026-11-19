using Course;

internal class Program {
    private static int Main(string[] args) {
        if (args.Contains("--self-test")) {
            return SelfTests.Run();
        }
        if (args.Contains("--dispatch")) {
            DispatchDemo.Run();
            return 0;
        }
        Console.WriteLine("Lernbeispiel Polymorphie; keine sichere Verschlüsselung.");
        var text = "Hallo ZbW!";
        ICryptoAlgorithm cipher = new CaesarCipher(3);
        var service = new MessageService(Console.Out);
        service.SendMessage(cipher, text);
        Console.WriteLine(cipher.Decrypt(cipher.Encrypt(text)));
        cipher = new ViginereCipher("KEY");
        service.SendMessage(cipher, text);
        Console.WriteLine(cipher.Decrypt(cipher.Encrypt(text)));

        if (args.Contains("--file")) {
            using (var writer = new StreamWriter("nachricht.txt", false)) {
                new MessageService(writer).SendMessage(cipher, text);
            }
            var encrypted = File.ReadAllText("nachricht.txt").TrimEnd('\r', '\n');
            Console.WriteLine(cipher.Decrypt(encrypted));
        }
        return 0;
    }
}
