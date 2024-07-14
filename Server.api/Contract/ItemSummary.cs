namespace Server.api.Contract;

public record class ItemSummary (
  string Id, 
  string Name, 
  string Category, 
  decimal Price, 
  DateTime DateAdd);
