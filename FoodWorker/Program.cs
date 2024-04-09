using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;
using System.Threading.Channels;

namespace FoodWorker {
  internal class Program {
    static void Main(string[] args) {
      Console.WriteLine("ok");
      Thread.Sleep(90000);
      Console.WriteLine("ok ok");
      var config = ReadConfig.ReadRabbitConfig();

      IBus bus = RabbitHutch.CreateBus(
      config["HOST_RABBITMQ"], "food_topic", ExchangeType.Topic,
      Convert.ToUInt16(config["PORT_RABBITMQ"]),
      config["RABBITMQ_DEFAULT_USER"], config["RABBITMQ_DEFAULT_PASS"]);
      ConsumerFood consumer = new ConsumerFood(bus);

      var queueRoutingKeyPairs = new List<(string, string)>{
        ("italian_food","food.italian" ),
        ("ukrainian_food", "food.ukrainian" ),
        ("mexican_food", "food.mexican"),
      };
      bus.BindQueuesToRoutingKeys(queueRoutingKeyPairs);

      Console.WriteLine(" [*] Waiting for messages. Press enter to end");
      Console.ReadLine();

      while (true) ;
    }
  }
}
