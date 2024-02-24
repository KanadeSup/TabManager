using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
   public class Bookmark
   {
      [Key]
      public Guid Id { get; set; }
      
      public string? Title { get; set; }
      
      [Required]
      public required string Url { get; set; }
      
      public string? Description { get; set; }
      
      public byte[]? WebIcon { get; set; }
      
      [ForeignKey("Category_Id")]
      [Required]
      public Category Category { get; set; } = null!;
   }
}