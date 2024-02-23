using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
   public class Category
   {
      [Key]
      public Guid Id { get; set; }
      
      [Required]
      public required string Name { get; set; }
      
      public ICollection<Bookmark>? Bookmarks { get; set; }

      [ForeignKey("Space_Id")]
      [Required]
      public required Space Space { get; set; }
   }
}
