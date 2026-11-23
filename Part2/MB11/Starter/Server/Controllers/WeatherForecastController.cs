using Microsoft.AspNetCore.Mvc;

namespace ProgAdvII.HttpApi.Controllers;

[ApiController]
[Route("weatherforecast")]
public sealed class WeatherForecastController(IForecastService forecasts) : ControllerBase {
    [HttpGet]
    public ActionResult<WeatherForecast[]> Get() {
        return Ok(forecasts.Create(5));
    }
}
