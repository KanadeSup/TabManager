using Core.DTO;

namespace Core.ServiceContracts
{
   public interface IAuthenticationService
   {
      public Task CreateAccountAsync(RegisterDTO registerDTO);
      public Task<TokenDTO> SignInAsync(LoginDTO loginDTO);
      public Task<TokenDTO> RefreshTokenAsync(string refreshToken);
   }
}