using Core.ServiceContracts;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Core
{
   public static class ConfigureService
   {
      public static IServiceCollection AddServices(this IServiceCollection services)
      {
         services.AddHttpContextAccessor();
         services.AddScoped<IAuthenticationService, AuthenticationService>();
         services.AddScoped<IJwtService, JwtService>();
         services.AddScoped<ISpaceService, SpaceService>();
         services.AddScoped<ICurrentUserService, CurrentUserService>();
         return services;
      }
   }
}