using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.IO;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class SendRequestWeather : ControllerBase {
    private readonly ILogger<SendRequestWeather> _logger;
    private readonly IBus _busControl;

    public SendRequestWeather(ILogger<SendRequestWeather> logger) {
      _logger = logger;

      // read config
      var config = ReadConfig.ReadRabbitConfig();

      _busControl = RabbitHutch.CreateBus(
          config["HOST_RABBITMQ"],
          "weather_direct",
          ExchangeType.Direct,
          Convert.ToUInt16(config["PORT_RABBITMQ"]),
          config["RABBITMQ_DEFAULT_USER"],
          config["RABBITMQ_DEFAULT_PASS"]
      );
    }

    [HttpPost("{route}")]
    async public Task<IActionResult> SendMessage(
        [FromRoute] string route,
        [FromBody] string message
    ) {
      try {
        await _busControl.SendAsync(route, message);
        return Ok($"Sent message to {route}");
      }
      catch (Exception ex) {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
}
