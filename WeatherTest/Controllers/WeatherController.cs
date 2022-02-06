using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherTest.Attributes;
using WeatherTest.Contracts;
using WeatherTest.Models;

namespace WeatherTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {

        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            this.weatherService = weatherService;
        }

        [HttpPut("GetMedianValues")]
        public async Task<IActionResult> GetMedianValues([FromBody] WeatherRequest request)
        {
            WeatherResponse weatherResponse = null;
            if (request.IsValid)
            {
                weatherResponse = await weatherService.GetMedianValues(request);
            }
            if (weatherResponse != null)
            {
                return Ok(weatherResponse);
            }
            return BadRequest("Too many entries, the limit is 20 inserts");
            
        }
    }
}
