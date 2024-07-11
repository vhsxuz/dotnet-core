namespace Server.api.Contract;

public record class UpdateItem(  
  string Name, 
  string Category, 
  decimal Price, 
  DateTime DateAdd);
