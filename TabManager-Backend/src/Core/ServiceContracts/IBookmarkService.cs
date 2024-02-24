using Core.DTO.Category;

namespace Core.ServiceContracts
{
   public interface IBookmarkService
   {
      Task<BookmarkResponseDTO> AddBookmarkAsync(Guid categoryId, AddBookmarkDTO bookmark);
      Task DeleteBookmarkAsync(Guid bookmarkId);
      Task<BookmarkResponseDTO?> GetBookmarkByIdAsync(Guid bookmarkId);
      Task<List<BookmarkResponseDTO>> GetBookmarksAsync(Guid categoryId);
      Task<BookmarkResponseDTO> UpdateBookmarkAsync(Guid bookmarkId, UpdateBookmarkDTO bookmark);
   }
}