using System.Security.Cryptography;
using System.Text;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Core.ServiceContracts;
using Common.Exceptions;

namespace Core.Services
{
   public class AuthenticationService : IAuthenticationService
   {
      private readonly IAccountRepository _accountRepository;
      private readonly IJwtService _jwtService;
      private readonly int _iterationCount = 10000;
      private readonly int _keySize = 64;

      public AuthenticationService(IAccountRepository accountRepository, IJwtService jwtService)
      {
         _accountRepository = accountRepository;
         _jwtService = jwtService;
      }
      public async Task<string> SignInAsync(LoginDTO loginDTO)
      {
         var userAccount = await _accountRepository.GetAccountByEmail(loginDTO.Email);
         if(userAccount == null)
            throw new AccountIsInvalidException("Email is not existed.");
         var hashedPassword = HashPassword(loginDTO.Password, userAccount.Salt);
         if(hashedPassword != userAccount.HashedPassword)
            throw new AccountIsInvalidException("Invalid password.");

         return _jwtService.GenerateJwtToken(userAccount);
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
   }
}