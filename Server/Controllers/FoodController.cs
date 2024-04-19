using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Model;
using RabbitMQ.Client;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("/food")]
  public class FoodController : ControllerBase {
    private readonly ILogger<WeatherController> _logger;
    private readonly IBus _busControl;

    public FoodController(ILogger<WeatherController> logger) {
      _logger = logger;
      _busControl = RabbitHutch.CreateBus(
         Environment.GetEnvironmentVariable("RABBITMQ_HOST")!,
         "food_topic",
         ExchangeType.Topic,
         ushort.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")!),
         Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER")!,
         Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")!
         );
    }

    [HttpPost("{route}")]
    public IActionResult PostMessage(
        [FromRoute] string route,
        [FromBody] FoodModel inputFood
    ) {
      var message = new FoodRequest(inputFood);
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
