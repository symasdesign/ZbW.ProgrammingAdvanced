using System.Globalization;
using System.Text;
using Course.Shop;

internal class Program {
    private static void Main(string[] args) {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var mode = args.Length == 0 ? "all" : args[0];
        if (mode is not ("all" or "shop" or "mastercard" or "graph")) {
            Console.Error.WriteLine("Modus: all | shop | mastercard | graph.");
            Environment.ExitCode = 2;
            return;
        }

        void Run(string id, Action action) {
            if (mode != "all" && mode != id)
                return;
            Console.WriteLine($"--- {id} ---");
            action();
        }

        var terminal = new EftTerminal();
        Run("shop", () => {
            var shopper = CompositionRoot.CreateShopper(new Visa(terminal));
            Console.WriteLine(shopper.Checkout(25m));
        });
        Run("mastercard", () => {
            var shopper = CompositionRoot.CreateShopper(new MasterCard(terminal));
            Console.WriteLine(shopper.Checkout(25m));
        });
        Run("graph", () => {
            foreach (var type in new[] { typeof(Shopper), typeof(Visa), typeof(MasterCard) }) {
                var constructor = type.GetConstructors().Single();
                foreach (var parameter in constructor.GetParameters())
                    Console.WriteLine($"{type.Name} -> {parameter.ParameterType.Name}");
            }
        });
    }
}
