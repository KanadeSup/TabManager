using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
   public class Space
   {
      [Key]
      public Guid Id { get; set; }
      
      public string? Name { get; set; }
      
      public ICollection<Category>? Categories { get; set; }
   }
}