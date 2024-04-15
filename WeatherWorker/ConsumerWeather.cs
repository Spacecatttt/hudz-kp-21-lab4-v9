using hudz_kp_21_lab4_v9.RabbitMq;

namespace WeatherWorker {
  public class ConsumerWeather {
    private readonly IBus _busControl;
    public ConsumerWeather(IBus busControl) {
      _busControl = busControl;
      this.Binding();
    }

    public void Binding() {
      _busControl.Receive<string>("weather_us_west", ReceiveWeatherUsWest);
      _busControl.Receive<string>("weather_us_east", ReceiveWeatherUsEast);
      _busControl.Receive<string>("weather_uk", ReceiveWeatherUK);
      _busControl.Receive<string>("weather_world", ReceiveWeatherWorld);
    }

    public void ReceiveWeatherUsWest<T>(T message) {
      Log($"Received message (weather:us:west): {message?.ToString()}");
    }

    public void ReceiveWeatherUsEast<T>(T message) {
      Log($"Received message (weather:us:east): {message?.ToString()}");
    }

    public void ReceiveWeatherUK<T>(T message) {
      Log($"Received message (weather:uk): {message?.ToString()}");
    }

    public void ReceiveWeatherWorld<T>(T message) {
      Log($"Received message (weather:world): {message?.ToString()}");
    }

    public void Log(string message) {
      string id = Guid.NewGuid().ToString();
      string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
      Console.WriteLine($"{id} {timestamp} {message}");
    }
  }
}
