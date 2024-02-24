using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Category
{
   public class UpdateBookmarkDTO
   {
      public string? Title { get; set; }

      [Required]
      public string Url { get; set; } = null!;

      public string? Description { get; set; }
      
      public byte[]? WebIcon { get; set; }
   }
}