using System.ComponentModel.DataAnnotations;

namespace Server.api.Contract;

public record class CreateItem ( 
  [Required] [StringLength(50)]string Name, 
  string CategoryId, 
  [Range(1, 100)]decimal Price, 
  DateTime DateAdd);
