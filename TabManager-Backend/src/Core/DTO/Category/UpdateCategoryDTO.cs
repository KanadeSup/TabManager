using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Category
{
   public class UpdateCategoryDTO
   {
      [Required]
      public string Name { get; set; } = null!;
   }
}