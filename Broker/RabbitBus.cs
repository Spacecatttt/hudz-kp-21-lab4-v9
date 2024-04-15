using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
    public void Send<T>(string routingKey, T message) {
      _channel.CreateBasicProperties().Persistent = false;
      var output = JsonConvert.SerializeObject(message);
      _channel.BasicPublish(_exchange, routingKey, null, Encoding.UTF8.GetBytes(output));
    }
    public void Receive<T>(string queue, Action<T> onMessage) {
      var consumer = new EventingBasicConsumer(_channel);

      consumer.Received += (s, e) => {
        var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
        var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
        onMessage(item);
      };
      string consumerTag = _channel.BasicConsume(queue, true, consumer);
    }
    public void BindQueuesToRoutingKeys(List<(string, string)> queueRoutingKeyPairs) {
      foreach (var (queue, routingKey) in queueRoutingKeyPairs) {
        _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: queue, exchange: _exchange, routingKey: routingKey);
      }
    }
  }
}
