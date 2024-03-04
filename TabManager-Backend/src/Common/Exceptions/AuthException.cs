namespace Common.Exceptions
{
   public class AccountIsInvalidException : CustomException
   {
      public AccountIsInvalidException(string message) : base(message)
      {}
   }

   public class AccountIsExistedException : CustomException
   {
      public AccountIsExistedException(string message) : base(message)
      {}
   }

   public class UnauthorizedException : CustomException
   {
      public UnauthorizedException(string message) : base(message, 401)
      {}
   }

   public class InvalidRefreshTokenException : CustomException
   {
      public InvalidRefreshTokenException(string message) : base(message)
      {}
   }
}