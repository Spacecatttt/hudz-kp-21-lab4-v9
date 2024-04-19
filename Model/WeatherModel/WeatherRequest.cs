namespace Model {
  public class WeatherRequest {
    public Guid Uuid { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public WeatherRequest() { }
    public WeatherRequest(WeatherModel weather) {
      Uuid = Guid.NewGuid();
      DateTimeStamp = DateTime.UtcNow;
      MinTemperature = weather.MinTemperature;
      MaxTemperature = weather.MaxTemperature;
    }
  }
}
