namespace Server.api.Contract;

public record class Item (
  string Id, 
  string Name, 
  string Category, 
  decimal Price, 
  DateTime DateAdd);
