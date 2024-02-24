namespace Common.Exceptions
{
   public class BookmarkNotFoundException : CustomException
   {
      public BookmarkNotFoundException(string message) : base(message,404)
      {
      }
   }
}