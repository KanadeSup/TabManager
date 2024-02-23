using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
   public class UserAccount
   {
      [Key]
      public Guid Id { get; set; }

      [Required]
      public required string Email { get; set; }

      [Required]
      public required string HashedPassword { get; set; }

      [Required]
      public required byte[] Salt { get; set; }

      [Required]
      public required bool IsEmailVerified { get; set; }

      public string? DisplayName { get; set; }
      
      public byte[]? Avatar { get; set; }

      public ICollection<Space>? Spaces { get; set; }
   }
}