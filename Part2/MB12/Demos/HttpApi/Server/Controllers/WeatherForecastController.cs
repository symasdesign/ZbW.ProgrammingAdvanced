using Microsoft.AspNetCore.Mvc;

namespace ProgAdvII.HttpApi.Controllers;

[ApiController]
[Route("weatherforecast")]
public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet]
    public ActionResult<WeatherForecast[]> Get([FromQuery] int days = 5) {
        if (days < 1 || days > 7) {
            return BadRequest(new {
                error = "days muss zwischen 1 und 7 liegen."
            });
        }

        // Demodaten: DateOnly und LINQ aus dem vorhandenen WeatherForecast-Beispiel.
        var startDate = DateOnly.FromDateTime(DateTime.Today);

        var forecasts = Enumerable.Range(1, days)
            .Select(index => new WeatherForecast {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        return Ok(forecasts);
    }
}
