using hudz_kp_21_lab4_v9.RabbitMq;
using Model;

namespace FoodWorker {
  internal class ConsumerFood {
    private readonly IBus _busControl;
    public ConsumerFood(IBus busControl) {
      _busControl = busControl;
      this.Binding();
    }

    public void Binding() {
      _busControl.Receive<FoodRequest>("italian_food", ReceiveFoodItalian);
      _busControl.Receive<FoodRequest>("ukrainian_food", ReceiveFoodUkrainian);
      _busControl.Receive<FoodRequest>("mexican_food", ReceiveFoodMexican);
    }

    public void ReceiveFoodItalian(FoodRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: The name of the dish in Italian cuisine - {message.Name}, descripion - {message.Description}");
    }

    public void ReceiveFoodUkrainian(FoodRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: The name of the dish in Ukrainian cuisine - {message.Name}, descripion - {message.Description}");
    }

    public void ReceiveFoodMexican(FoodRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: The name of the dish in Mexican cuisine - {message.Name}, descripion - {message.Description}");
    }
  }
}
