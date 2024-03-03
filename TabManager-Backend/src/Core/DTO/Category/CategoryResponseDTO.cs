namespace Core.DTO.Category
{
   public class CategoryResponseDTO
   {
      public Guid Id { get; set; }
      public string Name { get; set; } = null!;
      public DateTime CreationTime {get; set;}
   }
}