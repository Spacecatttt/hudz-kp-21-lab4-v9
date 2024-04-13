using System.Runtime.InteropServices;
using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;

namespace WeatherWorker {
  internal class Program {
    static void Main(string[] args) {
      var config = ReadConfig.ReadRabbitConfig();

      IBus bus = AttemptRabbitMQConnection(config);

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

      ConsumerWeather consumer = new ConsumerWeather(bus);

      Console.WriteLine(" [*] Waiting for messages. Press enter to end");
      Console.ReadLine();
      while (true)
        ;
    }

    static IBus AttemptRabbitMQConnection(Dictionary<string, string> config) {
      int retries = 10;
      int retryIntervalSeconds = 10;
      for (int i = 0; i < retries; i++) {
        try {
          Console.WriteLine("Attempting connection...");
          IBus bus = RabbitHutch.CreateBus(
              config["HOST_RABBITMQ"],
              "weather_direct",
              ExchangeType.Direct,
              Convert.ToUInt16(config["PORT_RABBITMQ"]),
              config["RABBITMQ_DEFAULT_USER"],
              config["RABBITMQ_DEFAULT_PASS"]
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
