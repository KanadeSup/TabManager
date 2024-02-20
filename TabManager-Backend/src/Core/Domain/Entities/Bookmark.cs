using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
   public class Bookmark
   {
      [Key]
      public Guid Id { get; set; }
      
      public string? Title { get; set; }
      
      public string? Url { get; set; }
      
      public string? Description { get; set; }
      
      public byte[]? WebIcon { get; set; }
      
      public DateTime CreatedAt { get; set; }
      
      public DateTime UpdatedAt { get; set; }
      
      [ForeignKey("UserAccount_Id")]
      public required UserAccount UserAccount { get; set; }

      [ForeignKey("Category_Id")]
      public required Category Category { get; set; }
   }
}