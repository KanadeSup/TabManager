using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Category
{
   public class AddCategoryDTO
   {
      [Required]
      public string Name { get; set; } = null!;
   }
}