using System.ComponentModel.DataAnnotations;

namespace Server.api.Contract;

public record class UpdateItem(  
  [Required] [StringLength(50)]string Name, 
  [Required] [StringLength(20)] string Category, 
  [Range(1, 100)]decimal Price, 
  DateTime DateAdd);
