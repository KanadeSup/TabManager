namespace Core.DTO
{
   public class ValidateErrorResponse
   {
      public required string Field { get; set; }
      public required List<string> Messages { get; set; }
   }
}