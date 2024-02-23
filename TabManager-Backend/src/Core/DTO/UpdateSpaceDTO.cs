using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
   public class UpdateSpaceDTO
      {
         [Required]
         public string Name { get; set; } = null!;

         [Required]
         [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid hex color.")]
         public string HexColor { get; set; } = null!;
      }
}