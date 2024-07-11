namespace Server.api.Contract;

public record class CreateItem ( 
  string Name, 
  string Category, 
  decimal Price, 
  DateTime DateAdd);
