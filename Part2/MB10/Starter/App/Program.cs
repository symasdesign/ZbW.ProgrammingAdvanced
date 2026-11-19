using Course.Container;
using Course.Shop;

internal class Program {
    private static void Main(string[] args) {
        var mode = args.FirstOrDefault() ?? "graph";
        var container = new MiniContainer();
        try {
            if (mode == "cycle") {
                container.Register<CycleA, CycleA>();
                container.Register<CycleB, CycleB>();
                container.Resolve<CycleA>();
            } else {
                container.Register<Shopper, Shopper>();
                container.Register<ICreditCard, Visa>();
                if (mode != "missing")
                    container.Register<ITerminal, EftTerminal>();
                if (mode == "mastercard")
                    container.Register<ICreditCard, MasterCard>();
                var first = container.Resolve<Shopper>();
                var second = container.Resolve<Shopper>();
                Console.WriteLine(first.Checkout(25m));
                Console.WriteLine($"Gleicher Shopper: {ReferenceEquals(first, second)}");
            }
        } catch (Exception exception) when (exception is InvalidOperationException or NotImplementedException) {
            Console.WriteLine(exception.Message);
        }
    }
}
