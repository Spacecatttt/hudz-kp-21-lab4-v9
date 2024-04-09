using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;

namespace hudz_kp_21_lab4_v9_1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("ok");
      Thread.Sleep(90000);
      var config = ReadConfig.ReadRabbitConfig();

      IBus bus = RabbitHutch.CreateBus(
       config["HOST_RABBITMQ"], "weather_direct", ExchangeType.Direct,
      Convert.ToUInt16(config["PORT_RABBITMQ"]),
      config["RABBITMQ_DEFAULT_USER"], config["RABBITMQ_DEFAULT_PASS"]);

      var queueRoutingKeyPairs = new List<(string, string)>{
        ("weather_us_west","weather:us:west" ),
        ("weather_us_east", "weather:us:east" ),
        ("weather_uk", "weather:uk"),
        ("weather_world", "weather:world"),
        ("weather_us_west", "weather:us"),
        ("weather_us_east", "weather:us"),
      };

      bus.BindQueuesToRoutingKeys(queueRoutingKeyPairs);

      ConsumerWeather consumer = new ConsumerWeather(bus);

      Console.WriteLine(" [*] Waiting for messages. Press enter to end");
      Console.ReadLine();
      while (true) ;

    }

  }
}
