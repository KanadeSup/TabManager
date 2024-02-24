using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTO.Category
{
   public class AddBookmarkDTO
   {
      public string? Title { get; set; }

      [Required]
      public string Url { get; set; } = null!;

      public string? Description { get; set; }
      
      public IFormFile? Icon { get; set; }
   }
}