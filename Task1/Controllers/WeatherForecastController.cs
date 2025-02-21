using Microsoft.AspNetCore.Mvc;
using Task1.Services;

namespace Task1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(HangfireService hangfireService,
        ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private readonly HangfireService _hangfireService = hangfireService;
        private readonly ILogger<WeatherForecastController> _logger = logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _hangfireService.SimpleJob();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("schedule")]
        public IActionResult Sechudleing()
        {
            _hangfireService.SendEmails();
            return Ok();
        }
    }
}
