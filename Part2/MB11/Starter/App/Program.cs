using Course.Container;
using Course.Shop;
using Course.Lifetimes;
using Microsoft.Extensions.DependencyInjection;

internal class Program {
    private static void Main(string[] args) {
        var mode = args.FirstOrDefault() ?? "shop";
        if (mode == "own") {
            var own = new MiniContainer();
            own.Register<Shopper, Shopper>();
            own.Register<ICreditCard, Visa>();
            own.Register<ITerminal, EftTerminal>();
            Console.WriteLine(own.Resolve<Shopper>().Checkout(25m));
        } else if (mode == "shop") {
            var services = new ServiceCollection();
            services.AddTransient<ITerminal, EftTerminal>();
            services.AddTransient<ICreditCard, Visa>();
            services.AddTransient<Shopper>();
            using var provider = services.BuildServiceProvider();
            Console.WriteLine(provider.GetRequiredService<Shopper>().Checkout(25m));
        } else {
            LifetimeDemo.Run(mode);
        }
    }
}
