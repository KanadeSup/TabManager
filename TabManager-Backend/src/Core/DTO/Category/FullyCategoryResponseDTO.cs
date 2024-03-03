namespace Core.DTO.Category
{
   public class FullyCategoryResponseDTO : CategoryResponseDTO
   {
      public IEnumerable<BookmarkResponseDTO> Bookmarks { get; set; } = new List<BookmarkResponseDTO>();
   }
}