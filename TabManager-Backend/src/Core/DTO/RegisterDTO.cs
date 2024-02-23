using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class RegisterDTO
    {
      [Required]
      [EmailAddress]
      [StringLength(100, MinimumLength = 5)]
      public string Email { get; set; } = null!;

      [Required]
      [StringLength(100, MinimumLength = 6)]
      public string Password { get; set; } = null!;

      [StringLength(50, MinimumLength = 3)]
      public string? displayName { get; set; }
    }
}