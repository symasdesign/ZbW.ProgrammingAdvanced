using ProgAdvII.HttpApi;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddScoped<IForecastService, ForecastService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
