using Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
namespace Core.Services
{
   public class CurrentUserService : ICurrentUserService
   {
      private readonly IHttpContextAccessor _httpContextAccessor;
      public CurrentUserService(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }
      public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;
      public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;
   }
}