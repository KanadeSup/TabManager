using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Category
{
   public class BookmarkResponseDTO
   {
      public required Guid Id { get; set; }

      public string? Title { get; set; }

      [Required]
      public string Url { get; set; } = null!;

      public string? Description { get; set; }
      
      public byte[]? WebIcon { get; set; }
   }
}