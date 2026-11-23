using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "hex" or "extended" or "cases")) {
            Console.Error.WriteLine("Modus: all | hex | extended | cases.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            try {
                action();
            } catch (NotImplementedException) {
                Console.WriteLine("TODO: Aufgabenmethode vervollständigen.");
            }
        }

        var samples = new[] { "#123", "ABCDEF", "#abc", "  #123  ", "#12345", "#12G", "x#ABCDEFx", "" };
        Run("hex", () => {
            foreach (var text in samples)
                Console.WriteLine($"'{text}': {Operations.IsHexUpper(text)}");
        });
        Run("extended", () => {
            foreach (var text in samples)
                Console.WriteLine($"'{text}': {Operations.IsHex(text)}");
        });
        Run("cases", () => {
            foreach (var text in samples)
                Console.WriteLine($"'{text}': upper={Operations.IsHexUpper(text)}, extended={Operations.IsHex(text)}");
        });
    }
}

public static class Operations {
    public static bool IsHexUpper(string text) {
        // TODO: ganzes HEX-Color-Muster formulieren.
        throw new NotImplementedException();
    }
    public static bool IsHex(string text) {
        // TODO: vollständiges Pattern mit Whitespace und IgnoreCase.
        throw new NotImplementedException();
    }
}
