using System;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPP.API.Controllers
{
	[ApiController, Route("api")]
	public class BaseController : ControllerBase
	{
		public IActionResult ResponseHandler<T>(T data) where T : class
		{
			return data == null ? NotFound() : Ok(data);
		}
	}
}