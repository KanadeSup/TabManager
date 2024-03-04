using Core.Domain.Entities;
using Core.DTO;

namespace Core.Domain.RepositoryContracts
{
   public interface IAccountRepository
   {
      public Task AddAccount(UserAccount acc);
      public Task<UserAccount?> GetAccountByEmail(string email);
      public Task<UserAccount?> GetAccountById(Guid id);
      public Task UpdateRefreshToken(Guid id, string refreshToken, DateTime expiredAt);
      public Task<UserAccount?> GetAccountByRefreshToken(string refreshToken);
   }
}