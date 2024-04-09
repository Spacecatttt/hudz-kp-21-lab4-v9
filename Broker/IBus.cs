namespace hudz_kp_21_lab4_v9.RabbitMq {
  public interface IBus {
    Task SendAsync<T>(string queue, T message);
    Task ReceiveAsync<T>(string queue, Action<T> onMessage);
    void BindQueuesToRoutingKeys(List<(string, string)> queueRoutingKeyPairs);
  }
}
