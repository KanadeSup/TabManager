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

      public async Task<UserAccount?> GetAccountById(Guid id)
      {
         return await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == id);
      }

      public async Task UpdateRefreshToken(Guid id, string refreshToken, DateTime expiredAt)
      {
         var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == id);
         if(userAccount == null)
            throw new Exception("User is not existed.");
         userAccount.RefreshToken = refreshToken;
         userAccount.RefreshTokenExpiredAt = expiredAt;
         await _context.SaveChangesAsync();
      }
      
      public async Task<UserAccount?> GetAccountByRefreshToken(string refreshToken)
      {
         return await _context.UserAccounts
            .FirstOrDefaultAsync(
               x => x.RefreshToken == refreshToken && 
               x.RefreshTokenExpiredAt > DateTime.UtcNow
            );
      }
   }
}