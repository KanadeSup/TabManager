using Core.Domain.RepositoryContracts;
using Infrastructure.DbContex;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
   public static class DataContexService
   {
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<AnnotationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
         return services;
      }
      public static IServiceCollection AddRepositories(this IServiceCollection services)
      {
         services.AddScoped<IAccountRepository, AccountRepository>();
         services.AddScoped<ISpaceRepository, SpaceRepository>();
         services.AddScoped<ICategoryRepository, CategoryRepository>();
         services.AddScoped<IBookmarkRepository, BookmarkRepository>();
         return services;
      }
   }
}