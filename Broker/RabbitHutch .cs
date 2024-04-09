using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Channels;

namespace hudz_kp_21_lab4_v9.RabbitMq {

  public class RabbitHutch {
    private static ConnectionFactory _factory;
    private static IConnection _connection;
    private static IModel _channel;

    public static IBus CreateBus(string hostName) {
      _factory = new ConnectionFactory {
        HostName = hostName,
        DispatchConsumersAsync = true
      };
      _connection = _factory.CreateConnection();
      _channel = _connection.CreateModel();
      return new RabbitBus(_channel);
    }
    public static IBus CreateBus(string hostName, string exchangeName, string exchangeType) {
      _factory = new ConnectionFactory {
        HostName = hostName,
        DispatchConsumersAsync = true
      };
      _connection = _factory.CreateConnection();
      _channel = _connection.CreateModel();
      return new RabbitBus(_channel, exchangeName, exchangeType);
    }
    public static IBus CreateBus(
      string hostName, string exchangeName, string exchangeType,
      ushort hostPort, string username, string password) {
      _factory = new ConnectionFactory {
        HostName = hostName,
        Port = hostPort,
        UserName = username,
        Password = password,
        DispatchConsumersAsync = true
      };
      _connection = _factory.CreateConnection();
      _channel = _connection.CreateModel();
      return new RabbitBus(_channel, exchangeName, exchangeType);
    }
  }
}