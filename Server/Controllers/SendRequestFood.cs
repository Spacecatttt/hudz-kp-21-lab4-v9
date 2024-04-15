using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class SendRequestFood : ControllerBase {
    private readonly ILogger<SendRequestWeather> _logger;
    private readonly IBus _busControl;

    public SendRequestFood(ILogger<SendRequestWeather> logger) {
      _logger = logger;

      // read config
      var config = ReadConfig.ReadRabbitConfig();

      _busControl = RabbitHutch.CreateBus(
          config["HOST_RABBITMQ"],
          "food_topic",
          ExchangeType.Topic,
          Convert.ToUInt16(config["PORT_RABBITMQ"]),
          config["RABBITMQ_DEFAULT_USER"],
          config["RABBITMQ_DEFAULT_PASS"]
      );
    }

    [HttpPost("{route}")]
    public IActionResult SendMessage(
        [FromRoute] string route,
        [FromBody] string message
    ) {
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
