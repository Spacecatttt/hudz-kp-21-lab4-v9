using hudz_kp_21_lab4_v9.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.IO;

namespace hudz_kp_21_lab4_v9.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class GetReceiverWeather : ControllerBase
  {

    private readonly ILogger<GetReceiverWeather> _logger;
    private readonly IBus _busControl;
    public GetReceiverWeather(ILogger<GetReceiverWeather> logger)
    {
      _logger = logger;
      
      // read config
      var config = ReadConfig.ReadRabbitConfig();

      _busControl = RabbitHutch.CreateBus(
       config["HOST_RABBITMQ"], "weather_direct", ExchangeType.Direct,
      Convert.ToUInt16(config["PORT_RABBITMQ"]),
      config["RABBITMQ_DEFAULT_USER"], config["RABBITMQ_DEFAULT_PASS"]);
    }

    [HttpGet("weather:us:west")]
    async public Task<IActionResult> GetWeatherUsWest()
    {
      await _busControl.SendAsync("weather:us:west", "Get temperature in us:west");
      return Ok("sent message in us:west");
    }

    [HttpGet("weather:us:east")]
    public async Task<IActionResult> GetWeatherUsEast()
    {
      await _busControl.SendAsync("weather:us:east", "Get temperature in us:east");
      return Ok("sent message in us:east");
    }

    [HttpGet("weather:uk")]
    public async Task<IActionResult> GetWeatherUK()
    {
      await _busControl.SendAsync("weather:uk", "Get temperature in UK");
      return Ok("sent message in UK");
    }

    [HttpGet("weather:world")]
    public async Task<IActionResult> GetWeatherWorld()
    {
      await _busControl.SendAsync("weather:world", "Get temperature worldwide");
      return Ok("sent message worldwide");
    }

    [HttpGet("weather:us")]
    async public Task<IActionResult> GetWeatherUS()
    {
      await _busControl.SendAsync("weather:us", "Get temperature in us");
      return Ok("sent message in us");
    }
  }
}
