using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Application
{
   public static class ConfigureService
   {
      public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
      {
         services
            .AddAuthentication(
               options => {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               })
            .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = configuration["Jwt:Issuer"],
                  ValidAudience = configuration["Jwt:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
               };
            });
         services.AddAuthorization();
         return services;
      }
      public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
      {
         services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "TabManager API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
               In = ParameterLocation.Header,
               Description = "Please enter valid token",
               Name = "Authorization",
               Type = SecuritySchemeType.ApiKey,
               BearerFormat = "JWT",
               Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
               {
                  new OpenApiSecurityScheme
                  {
                     Reference = new OpenApiReference
                     {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                     }
                  },
                  new string[] {}
               }
            });
         });
         return services;
      }
   }
}