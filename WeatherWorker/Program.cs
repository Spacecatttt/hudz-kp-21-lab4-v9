using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace hudz_kp_21_lab4_v9_1 {
  internal class Program {
    static void Main(string[] args) {
      Console.WriteLine("ok");
      Thread.Sleep(60000);
      Console.WriteLine("not ok");
      IBus bus = RabbitHutch.CreateBus("localhost", "weather_topic", ExchangeType.Direct, 5672, "guest", "guest");
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
