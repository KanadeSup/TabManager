namespace Common.Exceptions
{
   public class CategoryNotFoundException : CustomException
   {
      public CategoryNotFoundException(string message) : base(message, 404)
      {}
   }
}