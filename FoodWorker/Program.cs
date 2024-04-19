using hudz_kp_21_lab4_v9.RabbitMq;
using RabbitMQ.Client;

namespace FoodWorker {
  internal class Program {
    static void Main(string[] args) {
      IBus? bus = AttemptRabbitMQConnection();
      if(bus is null){
        System.Console.WriteLine($"Connection to RabbitMq failed. Exit from program.");
        return;
      }

      var queueRoutingKeyPairs = new List<(string, string)>{
        ("italian_food","food.italian" ),
        ("ukrainian_food", "food.ukrainian" ),
        ("mexican_food", "food.mexican"),
      };

      bus.BindQueuesToRoutingKeys(queueRoutingKeyPairs);

      new ConsumerFood(bus);

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
            "food_topic",
            ExchangeType.Topic,
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
