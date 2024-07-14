namespace Server.api.Entities;

public class Item
{
  public required string Id { get; set;}
  public required string Name { get; set;}
  public required string CategoryId { get; set;}
  public Category? Category { get; set;}
  public decimal Price { get; set;}
  public DateTime DateAdd { get; set;}
}
