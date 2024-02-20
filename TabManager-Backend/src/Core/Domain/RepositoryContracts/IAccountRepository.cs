using Core.Domain.Entities;
using Core.DTO;

namespace Core.Domain.RepositoryContracts
{
   public interface IAccountRepository
   {
      public Task AddAccount(UserAccount acc);
      public Task<UserAccount?> GetAccountByEmail(string email);
   }
}