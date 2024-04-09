using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace hudz_kp_21_lab4_v9_1 {
  public class ConsumerWeather {
    private readonly IBus _busControl;
    public ConsumerWeather(IBus busControl) {
      _busControl = busControl;
      this.Binding();
    }

    public void Binding() {
      _busControl.ReceiveAsync<string>("weather_us_west", ReceiveWeatherUsWest);
      _busControl.ReceiveAsync<string>("weather_us_east", ReceiveWeatherUsEast);
      _busControl.ReceiveAsync<string>("weather_uk", ReceiveWeatherUK);
      _busControl.ReceiveAsync<string>("weather_world", ReceiveWeatherWorld);
    }

    async public void ReceiveWeatherUsWest<T>(T message) {
      await Log($"Received message (weather:us:west): {message?.ToString()}");
    }

    async public void ReceiveWeatherUsEast<T>(T message) {
      await Log($"Received message (weather:us:east): {message?.ToString()}");
    }

    async public void ReceiveWeatherUK<T>(T message) {
      await Log($"Received message (weather:uk): {message?.ToString()}");
    }

    async public void ReceiveWeatherWorld<T>(T message) {
      await Log($"Received message (weather:world): {message?.ToString()}");
    }

    async public Task Log(string message) {
      string id = Guid.NewGuid().ToString();
      string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
      await Console.Out.WriteLineAsync($"{id} {timestamp} {message}");
    }
  }
}
