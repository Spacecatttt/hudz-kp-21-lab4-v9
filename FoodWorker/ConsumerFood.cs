using hudz_kp_21_lab4_v9.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWorker {
  internal class ConsumerFood {
    private readonly IBus _busControl;
    public ConsumerFood(IBus busControl) {
      _busControl = busControl;
      this.Binding();
    }

    public void Binding() {
      _busControl.Receive<string>("italian_food", ReceiveFoodItalian);
      _busControl.Receive<string>("ukrainian_food", ReceiveFoodUkrainian);
      _busControl.Receive<string>("mexican_food", ReceiveFoodMexican);
    }

    public void ReceiveFoodItalian<T>(T message) {
      Log($"Received message (italian.food): {message.ToString()}");
    }

    public void ReceiveFoodUkrainian<T>(T message) {
      Log($"Received message (ukrainian.food): {message.ToString()}");
    }

    public void ReceiveFoodMexican<T>(T message) {
      Log($"Received message (mexican.food): {message.ToString()}");
    }
    public void Log(string message) {
      string id = Guid.NewGuid().ToString();
      string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
      Console.WriteLine($"{id} {timestamp} {message}");
    }
  }
}
