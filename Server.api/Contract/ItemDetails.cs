namespace Server.api.Contract;

public record class ItemDetails (
  string Id, 
  string Name, 
  string CategoryId, 
  decimal Price, 
  DateTime DateAdd);
