using Core.Domain.Entities;

namespace Core.ServiceContracts
{
   public interface IJwtService
   {
      public string GenerateJwtToken(UserAccount userAccount);
   }
}