using Core.DTO.Category;

namespace Core.Domain.RepositoryContracts {
   public interface IBookmarkRepository {
      Task<BookmarkResponseDTO> AddBookmarkAsync(Guid cateId, AddBookmarkDTO bookmarkDTO);
      Task DeleteBookmarkAsync(Guid bookmarkId);
      Task<BookmarkResponseDTO?> GetBookmarkByIdAsync(Guid bookmarkId);
      Task<List<BookmarkResponseDTO>> GetBookmarksAsync(Guid cateId);
      Task<BookmarkResponseDTO> UpdateBookmarkAsync(Guid bookmarkId, UpdateBookmarkDTO bookmark);
      Task<bool> IsBookmarkExistsAsync(Guid bookmarkId);
      Task<bool> IsBookmarkBelongsToUserAsync(Guid userId, Guid bookmarkId);
   }
}