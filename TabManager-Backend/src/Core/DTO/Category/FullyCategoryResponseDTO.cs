namespace Core.DTO.Category
{
   public class FullyCategoryResponseDTO : CategoryResponseDTO
   {
      public List<BookmarkResponseDTO> Bookmarks { get; set; } = new List<BookmarkResponseDTO>();
   }
}