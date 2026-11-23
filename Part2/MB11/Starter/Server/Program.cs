using ProgAdvII.HttpApi;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        // TODO: Registriere IForecastService mit ForecastService einmal pro HTTP-Anfrage.

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
