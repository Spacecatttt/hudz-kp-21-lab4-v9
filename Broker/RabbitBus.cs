using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace hudz_kp_21_lab4_v9.RabbitMq {
  public class RabbitBus : IBus {
    private readonly IModel _channel;
    private readonly string _exchange = string.Empty;
    internal RabbitBus(IModel channel) {
      _channel = channel;
    }
    internal RabbitBus(IModel channel, string exchangeName, string exchangeType) {
      _channel = channel;
      _exchange = exchangeName;
      _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
    }
    public async Task SendAsync<T>(string routingKey, T message) {
      await Task.Run(() => {
        _channel.QueueDeclare(routingKey, false, false, false);
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = false;
        var output = JsonConvert.SerializeObject(message);
        _channel.BasicPublish(_exchange, routingKey, null, Encoding.UTF8.GetBytes(output));
      });
    }
    public async Task ReceiveAsync<T>(string queue, Action<T> onMessage) {
      _channel.QueueDeclare(queue, false, false, false);
      var consumer = new AsyncEventingBasicConsumer(_channel);

      consumer.Received += async (s, e) => {
        var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
        var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
        onMessage(item);
        await Task.Yield();
      };
      string consumerTag = _channel.BasicConsume(queue, true, consumer);
      await Task.Yield();
    }
    public void BindQueuesToRoutingKeys(List<(string, string)> queueRoutingKeyPairs) {
      foreach (var (queue, routingKey) in queueRoutingKeyPairs) {
        _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: queue, exchange: _exchange, routingKey: routingKey);
      }
    }
  }
}
