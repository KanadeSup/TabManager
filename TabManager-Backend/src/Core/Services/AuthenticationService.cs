using System.Security.Cryptography;
using System.Text;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Core.ServiceContracts;
using Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
   public class AuthenticationService : IAuthenticationService
   {
      private readonly IAccountRepository _accountRepository;
      private readonly IJwtService _jwtService;
      private readonly int _iterationCount = 10000;
      private readonly int _keySize = 64;
      private readonly IConfiguration _configuration;

      public AuthenticationService(
         IAccountRepository accountRepository, 
         IJwtService jwtService, 
         IConfiguration configuration
      )
      {
         _accountRepository = accountRepository;
         _jwtService = jwtService;
         _configuration = configuration;
      }
      public async Task<TokenDTO> SignInAsync(LoginDTO loginDTO)
      {
         var userAccount = await _accountRepository.GetAccountByEmail(loginDTO.Email);
         if(userAccount == null)
            throw new AccountIsInvalidException("Email is not existed.");
         var hashedPassword = HashPassword(loginDTO.Password, userAccount.Salt);
         if(hashedPassword != userAccount.HashedPassword)
            throw new AccountIsInvalidException("Invalid password.");

         string refreshToken = GenerateRefreshToken();
         await _accountRepository.UpdateRefreshToken(
            userAccount.Id, 
            refreshToken, 
            DateTime.UtcNow.AddMinutes(GetRefreshTokenExpireMinute())
         );
         return new TokenDTO {
            Token = _jwtService.GenerateJwtToken(userAccount),
            RefreshToken = refreshToken
         };
      }

      public async Task<TokenDTO> RefreshTokenAsync(string refreshToken)
      {
         var userByRefreshToken = await _accountRepository.GetAccountByRefreshToken(refreshToken);
         if(userByRefreshToken == null)
            throw new InvalidRefreshTokenException("Invalid refresh token or expired.");

         string newRefreshToken = GenerateRefreshToken();
         await _accountRepository.UpdateRefreshToken(
            userByRefreshToken.Id, 
            newRefreshToken,
            DateTime.UtcNow.AddMinutes(GetRefreshTokenExpireMinute())
         );
         return new TokenDTO {
            Token = _jwtService.GenerateJwtToken(userByRefreshToken),
            RefreshToken = newRefreshToken
         };
      }

      public async Task CreateAccountAsync(RegisterDTO registerDTO)
      {
         var existingAccount = await _accountRepository.GetAccountByEmail(registerDTO.Email);
         if(existingAccount != null)
            throw new AccountIsExistedException("Account already exists.");
         var salt = RandomNumberGenerator.GetBytes(_keySize);
         var hashedPassword = HashPassword(registerDTO.Password, salt);
         var userAccount = new UserAccount
         {
            Email = registerDTO.Email,
            Salt = salt,
            HashedPassword = hashedPassword,
            IsEmailVerified = false,
         };
         await _accountRepository.AddAccount(userAccount);
      }

      private string HashPassword(string password, byte[] salt)
      {
         if(password == null || salt == null)
            throw new ArgumentNullException("The password and salt cannot be null.");

         var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterationCount,
            HashAlgorithmName.SHA256,
            _keySize
         );
         return Convert.ToHexString(hash);
      }

      private string GenerateRefreshToken()
      {
         var randomNumber = new byte[32];
         using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
         }
      }

      private int GetRefreshTokenExpireMinute()
      {
         string? refreshTokenExpireString = _configuration["Jwt:ExpireMinute_RefreshToken"];
         if(string.IsNullOrEmpty(refreshTokenExpireString))
            throw new Exception("Jwt:ExpireMinute_RefreshToken is missing in appsettings.json");
         return int.Parse(refreshTokenExpireString);
      }
   }
}