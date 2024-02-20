using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
   public class AccountRepository : IAccountRepository
   {
      private readonly AnnotationDbContext _context;
      public AccountRepository(AnnotationDbContext context)
      {
         _context = context;
      }

      public async Task AddAccount(UserAccount acc)
      {
         await _context.UserAccounts.AddAsync(acc);
         await _context.SaveChangesAsync();
      }

      public async Task<UserAccount?> GetAccountByEmail(string email)
      {
         if(email == null)
            throw new ArgumentNullException("Email cannot be null.");
         return await _context.UserAccounts.FirstOrDefaultAsync(x => x.Email == email);
      }
   }
}