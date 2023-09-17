using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAPP.Application.Services.OpenWeather;
using WeatherAPP.Infrastructure.Mediating;

namespace WeatherAPP.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class WeatherForecastController : BaseController
    {
        public WeatherForecastController(IServiceMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        [HttpGet("forecast")]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] string city)
        {
            var response = await mediator.Do<GetWeatherForecastRequest, GetWeatherForecastResponse>(new GetWeatherForecastRequest(city));
            return ResponseHandler(response);
        }
    }
}
