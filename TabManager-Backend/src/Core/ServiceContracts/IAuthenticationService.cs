using Core.DTO;

namespace Core.ServiceContracts
{
   public interface IAuthenticationService
   {
      public Task CreateAccountAsync(RegisterDTO registerDTO);
      public Task<string> SignInAsync(LoginDTO loginDTO);
   }
}