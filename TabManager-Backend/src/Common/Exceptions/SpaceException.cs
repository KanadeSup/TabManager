namespace Common.Exceptions
{
   public class SpaceNotFoundException : CustomException
   {
      public SpaceNotFoundException(string message) : base(message, 404)
      {}
   }
}