using Core.DTO.Category;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
   [Authorize]
   public class BookmarkController : ControllerBaseAPI 
   {
      private readonly IBookmarkService _bookmarkService;
      public BookmarkController(IBookmarkService bookmarkService)
      {
         _bookmarkService = bookmarkService;
      }

      [HttpGet("/api/categories/{categoryId}/bookmarks")]
      public async Task<IActionResult> GetBookmarksAsync(Guid categoryId)
      {
         var bookmarks = await _bookmarkService.GetBookmarksAsync(categoryId);
         return Ok(bookmarks);
      }

      [HttpGet("/api/bookmarks/{bookmarkId}")]
      public async Task<IActionResult> GetBookmarkByIdAsync(Guid bookmarkId)
      {
         var bookmark = await _bookmarkService.GetBookmarkByIdAsync(bookmarkId);
         return Ok(bookmark);
      }

      [HttpPost("/api/categories/{categoryId}/bookmarks")]
      public async Task<IActionResult> AddBookmarkAsync(Guid categoryId, [FromForm] AddBookmarkDTO bookmark)
      {
         var bookmarkDTO = await _bookmarkService.AddBookmarkAsync(categoryId, bookmark);
         return Ok(bookmarkDTO);
      }

      [HttpPut("/api/bookmarks/{bookmarkId}")]
      public async Task<IActionResult> UpdateBookmarkAsync(Guid bookmarkId, [FromBody] UpdateBookmarkDTO bookmark)
      {
         var bookmarkDTO = await _bookmarkService.UpdateBookmarkAsync(bookmarkId, bookmark);
         return Ok(bookmarkDTO);
      }

      [HttpDelete("/api/bookmarks/{bookmarkId}")]
      public async Task<IActionResult> DeleteBookmarkAsync(Guid bookmarkId)
      {
         await _bookmarkService.DeleteBookmarkAsync(bookmarkId);
         return NoContent();
      }
   }
}