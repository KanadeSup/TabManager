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

}