using Common.Exceptions;
using Core.Domain.RepositoryContracts;
using Core.DTO.Category;
using Core.ServiceContracts;

namespace Core.Services
{
   public class BookmarkService : IBookmarkService
   {
      private readonly IBookmarkRepository _bookmarkRepository;
      private readonly ICategoryRepository _categoryRepository;
      private readonly ICurrentUserService _currentUserService;
      public BookmarkService (
         IBookmarkRepository bookmarkRepository, 
         ICategoryRepository categoryRepository,
         ICurrentUserService currentUserService
      )
      {
         _bookmarkRepository = bookmarkRepository;
         _categoryRepository = categoryRepository;
         _currentUserService = currentUserService;
      }

      public async Task<BookmarkResponseDTO> AddBookmarkAsync(Guid cateId, AddBookmarkDTO bookmarkDTO)
      {
         var userId = _currentUserService.UserId;
         if(userId == null)
            throw new Exception("userId is not found in token");

         if(!await _categoryRepository.IsCategoryBelongToUserAsync(cateId, Guid.Parse(userId)))
            throw new CategoryNotFoundException("Category not found.");

         if(!await _categoryRepository.IsCategoryExistsAsync(cateId))
            throw new BookmarkNotFoundException("Category not found.");

         if(!await _categoryRepository.IsCategoryExistsAsync(cateId))
            throw new BookmarkNotFoundException("Category not found.");
         
         return await _bookmarkRepository.AddBookmarkAsync(cateId, bookmarkDTO);
      }

      public async Task DeleteBookmarkAsync(Guid bookmarkId)
      {
         var userId = _currentUserService.UserId;
         
         if(userId == null)
            throw new Exception("userId is not found in token");
         
         if(!await _bookmarkRepository.IsBookmarkBelongsToUserAsync(Guid.Parse(userId), bookmarkId))
            throw new BookmarkNotFoundException("Bookmark not found.");

         if(!await _bookmarkRepository.IsBookmarkExistsAsync(bookmarkId))
            throw new BookmarkNotFoundException("Bookmark not found.");

         await _bookmarkRepository.DeleteBookmarkAsync(bookmarkId);
      }

      public async Task<BookmarkResponseDTO?> GetBookmarkByIdAsync(Guid bookmarkId)
      {
         var userId = _currentUserService.UserId;
         if(userId == null)
            throw new Exception("userId is not found in token");
         
         if(!await _bookmarkRepository.IsBookmarkBelongsToUserAsync(Guid.Parse(userId), bookmarkId))
            throw new BookmarkNotFoundException("Bookmark not found.");

         if(!await _bookmarkRepository.IsBookmarkExistsAsync(bookmarkId))
            throw new BookmarkNotFoundException("Bookmark not found.");
         return await _bookmarkRepository.GetBookmarkByIdAsync(bookmarkId);
      }

      public async Task<List<BookmarkResponseDTO>> GetBookmarksAsync(Guid categoryId)
      {
         var userId = _currentUserService.UserId;
         if(userId == null)
            throw new Exception("userId is not found in token");
         
         if(!await _categoryRepository.IsCategoryBelongToUserAsync(categoryId, Guid.Parse(userId)))
            throw new CategoryNotFoundException("Category not found.");

         if(!await _categoryRepository.IsCategoryExistsAsync(categoryId))
            throw new BookmarkNotFoundException("Category not found.");

         return await _bookmarkRepository.GetBookmarksAsync(categoryId);
      }

      public async Task<BookmarkResponseDTO> UpdateBookmarkAsync(Guid bookmarkId, UpdateBookmarkDTO bookmark)
      {
         var userId = _currentUserService.UserId;
         if(userId == null)
            throw new Exception("userId is not found in token");
         
         if(!await _bookmarkRepository.IsBookmarkBelongsToUserAsync(bookmarkId, Guid.Parse(userId)))
            throw new BookmarkNotFoundException("Bookmark not found.");

         if(!await _bookmarkRepository.IsBookmarkExistsAsync(bookmarkId))
            throw new BookmarkNotFoundException("Bookmark not found.");
 
         return await _bookmarkRepository.UpdateBookmarkAsync(bookmarkId, bookmark);
      }
    }
}