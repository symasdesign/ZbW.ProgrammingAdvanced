namespace ProgAdvII.HttpApi;

public interface IForecastService {
    WeatherForecast[] Create(int days);
}

public sealed class ForecastService : IForecastService {
    private static readonly string[] Summaries =
        ["Freezing", "Chilly", "Cool", "Mild", "Warm", "Hot"];

    public WeatherForecast[] Create(int days) {
        var startDate = DateOnly.FromDateTime(DateTime.Today);
        return Enumerable.Range(1, days).Select(index => new WeatherForecast {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}
