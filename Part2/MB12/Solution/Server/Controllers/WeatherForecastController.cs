using Microsoft.AspNetCore.Mvc;

namespace ProgAdvII.HttpApi.Controllers;

[ApiController]
[Route("weatherforecast")]
public sealed class WeatherForecastController(IForecastService forecasts) : ControllerBase {
    [HttpGet]
    public ActionResult<WeatherForecast[]> Get([FromQuery] int days = 5) {
        if (days < 1 || days > 7)
            return BadRequest(new {
                error = "days muss zwischen 1 und 7 liegen."
            });
        return Ok(forecasts.Create(days));
    }
}
