using Core.ServiceContracts;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Core
{
   public static class ConfigureService
   {
      public static IServiceCollection AddServices(this IServiceCollection services)
      {
         services.AddScoped<IAuthenticationService, AuthenticationService>();
         services.AddScoped<IJwtService, JwtService>();
         return services;
      }
   }
}