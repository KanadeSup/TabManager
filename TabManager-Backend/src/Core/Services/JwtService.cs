using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Domain.Entities;
using Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
   public class JwtService : IJwtService
   {
      private readonly IConfiguration _configuration;

      public JwtService(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public string GenerateJwtToken(UserAccount userAccount)
      {
         var tokenHandler = new JwtSecurityTokenHandler();

         var issuer = _configuration["Jwt:Issuer"] ?? throw new Exception("Jwt:Issuer is missing in appsettings.json");
         var audience = _configuration["Jwt:Audience"] ?? throw new Exception("Jwt:Audience is missing in appsettings.json");
         var keyString = _configuration["Jwt:Key"] ?? throw new Exception("Jwt:Key is missing in appsettings.json");
         var key = Encoding.ASCII.GetBytes(keyString);
         var symmetricSecurityKey = new SymmetricSecurityKey(key);
         var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
         var expireString = _configuration["Jwt:ExpireMinute_AccessToken"];
         var expire = string.IsNullOrEmpty(expireString) ? 60 : int.Parse(expireString);

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new[] { 
               new Claim("userId", userAccount.Id.ToString()),
               new Claim("email", userAccount.Email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(expire),
            SigningCredentials = signingCredentials,
            Issuer = issuer,
            Audience = audience,
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }
   }
}