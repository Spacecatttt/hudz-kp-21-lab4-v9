using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace hudz_kp_21_lab4_v9.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class GetReceiverWeather : ControllerBase {

    private readonly ILogger<GetReceiverWeather> _logger;
    private readonly IBus _busControl;
    public GetReceiverWeather(ILogger<GetReceiverWeather> logger) {
      _logger = logger;
      _busControl = RabbitHutch.CreateBus("localhost", "weather_topic", ExchangeType.Direct, 5672, "guest", "guest");
    }

    [HttpGet("weather:us:west")]
    async public Task<IActionResult> GetWeatherUsWest() {
      await _busControl.SendAsync("weather:us:west", "Get temperature in us:west");
      return Ok("sent message in us:west");
    }

    [HttpGet("weather:us:east")]
    public async Task<IActionResult> GetWeatherUsEast() {
      await _busControl.SendAsync("weather:us:east", "Get temperature in us:east");
      return Ok("sent message in us:east");
    }

    [HttpGet("weather:uk")]
    public async Task<IActionResult> GetWeatherUK() {
      await _busControl.SendAsync("weather:uk", "Get temperature in UK");
      return Ok("sent message in UK");
    }

    [HttpGet("weather:world")]
    public async Task<IActionResult> GetWeatherWorld() {
      await _busControl.SendAsync("weather:world", "Get temperature worldwide");
      return Ok("sent message worldwide");
    }

    [HttpGet("weather:us")]
    async public Task<IActionResult> GetWeatherUS() {
      await _busControl.SendAsync("weather:us", "Get temperature in us");
      return Ok("sent message in us");
    }
  }
}
