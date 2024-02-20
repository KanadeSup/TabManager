using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContex
{
   public class AnnotationDbContext : DbContext
   {
      public AnnotationDbContext(DbContextOptions<AnnotationDbContext> options) : base(options)
      {
      }
      public DbSet<Bookmark> Bookmarks { get; set; }
      public DbSet<Category> Categories { get; set; }
      public DbSet<Space> Spaces { get; set; }
      public DbSet<UserAccount> UserAccounts { get; set; }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
      }
   }
}