using ProgAdvII.HttpApi;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Das Framework erstellt Controller und serialisiert ihre Antworten als JSON.
        builder.Services.AddControllers();
        builder.Services.AddScoped<IForecastService, ForecastService>();

        var app = builder.Build();

        // Middleware umschliesst den nachfolgenden Teil der Pipeline.
        app.Use(async (context, next) => {
            Console.WriteLine($"-> Request: {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");

            await next(context);

            Console.WriteLine($"<- Response: {context.Response.StatusCode}, {context.Response.ContentType ?? "(kein Content-Type)"}");
        });

        app.MapControllers();
        app.Run();
    }
}
