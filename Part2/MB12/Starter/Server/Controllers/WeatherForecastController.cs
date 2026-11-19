using Microsoft.AspNetCore.Mvc;

namespace ProgAdvII.HttpApi.Controllers;

[ApiController]
[Route("weatherforecast")]
public sealed class WeatherForecastController(IForecastService forecasts) : ControllerBase {
    [HttpGet]
    public ActionResult<WeatherForecast[]> Get() {
        // TODO: days mit Standardwert 5 aus Query lesen und auf 1..7 begrenzen.
        // Noch wird der Querywert ignoriert und immer die vorbereitete Folge erzeugt.
        return Ok(forecasts.Create(5));
    }
}
