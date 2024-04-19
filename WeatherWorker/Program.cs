using System.Runtime.InteropServices;
using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;

namespace WeatherWorker {
  internal class Program {
    static void Main(string[] args) {
      //var config = ReadConfig.ReadRabbitConfig();

      IBus? bus = AttemptRabbitMQConnection();
      if (bus is null) {
        System.Console.WriteLine($"Connection to RabbitMq failed. Exit from program.");
        return;
      }
      var queueRoutingKeyPairs = new List<(string, string)>
      {
        ("weather_us_west", "weather:us:west"),
        ("weather_us_east", "weather:us:east"),
        ("weather_uk", "weather:uk"),
        ("weather_world", "weather:world"),
        ("weather_us_west", "weather:us"),
        ("weather_us_east", "weather:us"),
      };

      bus.BindQueuesToRoutingKeys(queueRoutingKeyPairs);

      new ConsumerWeather(bus);

      Console.WriteLine(" [*] Waiting for messages. Press enter to end");
      Console.ReadLine();
      while (true) ;
    }

    static IBus? AttemptRabbitMQConnection() {
      int retries = 10;
      int retryIntervalSeconds = 10;
      for (int i = 0; i < retries; i++) {
        try {
          Console.WriteLine("Attempting connection...");
          IBus bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITMQ_HOST")!,
            "weather_direct",
            ExchangeType.Direct,
            ushort.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")!),
            Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER")!,
            Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS")!
          );

          Console.WriteLine("Connection successful!");
          return bus;
        }
        catch {
          Console.WriteLine(
              $"Connection failed. Retrying in {retryIntervalSeconds} seconds... Attempt {i + 1}/{retries}"
          );
        }
        Thread.Sleep(retryIntervalSeconds * 1000);
      }
      return null;
    }
  }
}
