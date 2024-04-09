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
      _busControl.ReceiveAsync<string>("italian_food", ReceiveFoodItalian);
      _busControl.ReceiveAsync<string>("ukrainian_food", ReceiveFoodUkrainian);
      _busControl.ReceiveAsync<string>("mexican_food", ReceiveFoodMexican);
    }

    async public void ReceiveFoodItalian<T>(T message) {
      await Log($"Received message (italian.food): {message.ToString()}");
    }

    async public void ReceiveFoodUkrainian<T>(T message) {
      await Log($"Received message (ukrainian.food): {message.ToString()}");
    }

    async public void ReceiveFoodMexican<T>(T message) {
      await Log($"Received message (mexican.food): {message.ToString()}");
    }
    async public Task Log(string message) {
      string id = Guid.NewGuid().ToString();
      string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
      await Console.Out.WriteLineAsync($"{id} {timestamp} {message}");
    }
  }
}
