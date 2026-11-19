using Microsoft.Extensions.DependencyInjection;

namespace Course.Lifetimes;

public static class LifetimeDemo {
    public static void Run(string mode) {
        var services = new ServiceCollection();
        switch (mode) {
            case "transient":
                services.AddTransient<UsageCounter>();
                break;
            case "singleton":
                services.AddSingleton<UsageCounter>();
                break;
            case "scoped":
                services.AddScoped<UsageCounter>();
                break;
            default:
                Console.WriteLine("Aufruf: own | shop | transient | singleton | scoped");
                return;
        }
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
        using (var firstScope = provider.CreateScope()) {
            var first = firstScope.ServiceProvider.GetRequiredService<UsageCounter>();
            var again = firstScope.ServiceProvider.GetRequiredService<UsageCounter>();
            Console.WriteLine($"Scope A: {first.Id}, {again.Id}; gleich: {ReferenceEquals(first, again)}");
        }
        using (var secondScope = provider.CreateScope()) {
            var second = secondScope.ServiceProvider.GetRequiredService<UsageCounter>();
            Console.WriteLine($"Scope B: {second.Id}");
        }
        Console.WriteLine("Beide Scopes verlassen; Provider wird nun freigegeben.");
    }
}
