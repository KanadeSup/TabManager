namespace Core.ServiceContracts
{
   public interface ICurrentUserService
   {
      string? UserId { get; }
      string? Email { get;}
   }
}