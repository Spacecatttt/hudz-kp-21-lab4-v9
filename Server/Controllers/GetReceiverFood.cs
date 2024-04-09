using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class GetReceiverFood : ControllerBase {

    private readonly ILogger<GetReceiverWeather> _logger;
    private readonly IBus _busControl;
    public GetReceiverFood(ILogger<GetReceiverWeather> logger) {
      _logger = logger;
      _busControl = RabbitHutch.CreateBus("localhost", "food_topic", ExchangeType.Topic);
    }

    [HttpGet("food.Italian")]
    async public Task<IActionResult> GetWeatherUsWest() {
      await _busControl.SendAsync("food.italian", "Get Italian food");
      return Ok("Sent in Italian food");
    }

    [HttpGet("food.Ukrainian")]
    public async Task<IActionResult> GetWeatherUsEast() {
      await _busControl.SendAsync("food.ukrainian", "Get Ukrainian food");
      return Ok("Sent in Ukrainian food");
    }

    [HttpGet("food.Mexican")]
    public async Task<IActionResult> GetWeatherUK() {
      await _busControl.SendAsync("food.mexican", "Get Mexican food");
      return Ok("Sent in Mexican food");
    }
  }
}
