using System;
using Microsoft.AspNetCore.Mvc;
using WeatherAPP.Infrastructure.Mediating;

namespace WeatherAPP.API.Controllers
{
	[ApiController, Route("api")]
	public class BaseController : ControllerBase
	{
        protected readonly IServiceMediator mediator;

        public BaseController(IServiceMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult ResponseHandler<T>(T data) where T : class
		{
			return data == null ? NotFound() : Ok(data);
		}
	}
}