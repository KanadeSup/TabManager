using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
   public class Space
   {
      [Key]
      public Guid Id { get; set; }
      
      [Required]
      public required string Name { get; set; }
      
      [Required]
      public required string HexColor { get; set; }

      public DateTime CreationTime {get; set;} = new DateTime();
      
      public ICollection<Category>? Categories { get; set; }

      [Required]
      public UserAccount userAccount { get; set; } = null!;
   }
}