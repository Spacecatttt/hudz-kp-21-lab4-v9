using hudz_kp_21_lab4_v9.RabbitMq;
using Model;

namespace WeatherWorker {
  public class ConsumerWeather {
    private readonly IBus _busControl;
    public ConsumerWeather(IBus busControl) {
      _busControl = busControl;
      this.Binding();
    }

    public void Binding() {
      _busControl.Receive<WeatherRequest>("weather_us_west", ReceiveWeatherUsWest);
      _busControl.Receive<WeatherRequest>("weather_us_east", ReceiveWeatherUsEast);
      _busControl.Receive<WeatherRequest>("weather_uk", ReceiveWeatherUK);
      _busControl.Receive<WeatherRequest>("weather_world", ReceiveWeatherWorld);
    }

    public void ReceiveWeatherUsWest(WeatherRequest message) {

      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: In us-west min. temprature = {message.MinTemperature}, max. temprature = {message.MaxTemperature}");
    }

    public void ReceiveWeatherUsEast(WeatherRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: In us-east min. temprature = {message.MinTemperature}, max. temprature = {message.MaxTemperature}");
    }

    public void ReceiveWeatherUK(WeatherRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: In uk min. temprature = {message.MinTemperature}, max. temprature = {message.MaxTemperature}");
    }

    public void ReceiveWeatherWorld(WeatherRequest message) {
      Console.WriteLine($"{message.Uuid} : {message.DateTimeStamp}: In world min. temprature = {message.MinTemperature}, max. temprature = {message.MaxTemperature}");
    }

  }
}
