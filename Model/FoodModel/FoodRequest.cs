namespace Model {
  public class FoodRequest {
    public Guid Uuid { get; set; }
    public DateTime DateTimeStamp { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FoodRequest() { }
    public FoodRequest(FoodModel food) {
      Uuid = Guid.NewGuid();
      DateTimeStamp = DateTime.UtcNow;
      Name = food.Name;
      Description = food.Description;
    }
  }
}
