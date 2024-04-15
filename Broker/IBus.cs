namespace hudz_kp_21_lab4_v9.RabbitMq {
  public interface IBus {
    void Send<T>(string queue, T message);
    void Receive<T>(string queue, Action<T> onMessage);
    void BindQueuesToRoutingKeys(List<(string, string)> queueRoutingKeyPairs);
  }
}
