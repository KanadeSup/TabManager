using System.ComponentModel.DataAnnotations;

namespace Core.DTO {
   public class LoginDTO {
      [Required]
      [EmailAddress]
      [StringLength(100)]
      public required string Email { get; set; }
      
      [Required]
      [StringLength(100, MinimumLength = 6)]
      public required string Password { get; set; }
   }
}