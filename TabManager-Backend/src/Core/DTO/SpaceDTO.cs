using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Core.DTO
{
   public class SpaceDTO
   {
      [Required]
      public Guid Id { get; set; }

      [Required]
      public string Name { get; set; } = null!;

      [Required]
      [RegularExpression(@"^#([A-Fa-f0-9]{6})$", ErrorMessage = "Invalid hex color.")]
      public string HexColor { get; set; } = null!;

      public DateTime CreationTime {get; set;}
   }
}