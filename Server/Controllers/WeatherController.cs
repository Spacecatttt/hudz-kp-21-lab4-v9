using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Model;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("/weather")]
  public class WeatherController : ControllerBase {
    private readonly ILogger<WeatherController> _logger;
    private readonly IBus _busControl;

    public WeatherController(ILogger<WeatherController> logger) {
      _logger = logger;

      _busControl = RabbitHutch.CreateBus(
         Environment.GetEnvironmentVariable("RABBITMQ_HOST")!,
         "weather_direct",
         ExchangeType.Direct,
         ushort.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")!),
         Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER")!,
         Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")!
         );

    }

    [HttpPost("{route}")]
    public IActionResult PostMessage(
        [FromRoute] string route,
        [FromBody] WeatherModel inputWeather) {
      if (inputWeather == null) {
        return BadRequest("Weather data is required.");
      }

      var message = new WeatherRequest(inputWeather);
      try {
        _busControl.Send(route, message);
        return Ok($"Sent message to {route}");
      }
      catch (Exception ex) {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
}
