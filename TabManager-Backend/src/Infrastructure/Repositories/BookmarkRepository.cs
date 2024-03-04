using Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO.Category;
using Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;

namespace Infrastructure.Repositories {
   public class BookmarkRepository : IBookmarkRepository
   {
      private readonly AnnotationDbContext _context;
      private readonly ICategoryRepository _categoryRepository;

      public BookmarkRepository(AnnotationDbContext context, ICategoryRepository categoryRepository)
      {
         _context = context;
         _categoryRepository = categoryRepository;
      }
      public async Task<BookmarkResponseDTO> AddBookmarkAsync(Guid cateId, AddBookmarkDTO bookmarkDTO)
      {
         var categoryById = await _context.Categories.Where(x=> x.Id == cateId).FirstOrDefaultAsync();
         if(categoryById == null)
            throw new Exception("Category not found.");


         var bookmark = new Bookmark 
         {
            Id = Guid.NewGuid(),
            Title = bookmarkDTO.Title,
            Description = bookmarkDTO.Description,
            Url = bookmarkDTO.Url,
            WebIcon = bookmarkDTO.Icon != null? await bookmarkDTO.Icon.GetBytes() : null,
            IconMineType = bookmarkDTO.Icon != null? bookmarkDTO.Icon.ContentType : null,
            Category = categoryById
         };            
         await _context.Bookmarks.AddAsync(bookmark);
         await _context.SaveChangesAsync();
         return new BookmarkResponseDTO
         {
            Id = bookmark.Id,
            Title = bookmark.Title,
            Description = bookmark.Description,
            Url = bookmark.Url,
            WebIcon = bookmark.WebIcon,
            IconMineType = bookmark.IconMineType
         };
      }

      public async Task DeleteBookmarkAsync(Guid bookmarkId)
      {
         var bookmark = await _context.Bookmarks.FirstOrDefaultAsync(x => x.Id == bookmarkId);
         if(bookmark == null)
            throw new Exception("Bookmark not found.");
         _context.Bookmarks.Remove(bookmark);
         await _context.SaveChangesAsync();
      }

      public async Task<BookmarkResponseDTO?> GetBookmarkByIdAsync(Guid bookmarkId)
      {
         var bookmark = await _context.Bookmarks.FirstOrDefaultAsync(x => x.Id == bookmarkId);
         if(bookmark == null)
            return null;
         return new BookmarkResponseDTO 
         {
            Id = bookmark.Id,
            Title = bookmark.Title,
            Description = bookmark.Description,
            Url = bookmark.Url,
            WebIcon = bookmark.WebIcon,
            IconMineType = bookmark.IconMineType
         };
      }

      public async Task<List<BookmarkResponseDTO>> GetBookmarksAsync(Guid cateId)
      {
         if(!await _categoryRepository.IsCategoryExistsAsync(cateId))
            throw new Exception("Category not found.");
         return await _context.Bookmarks
            .Where(x => x.Category.Id == cateId)
            .Select(x => new BookmarkResponseDTO
            {
               Id = x.Id,
               Title = x.Title,
               Description = x.Description,
               Url = x.Url,
               WebIcon = x.WebIcon,
               IconMineType = x.IconMineType
            })
            .ToListAsync();
      }

      public async Task<bool> IsBookmarkBelongsToUserAsync(Guid userId, Guid bookmarkId)
      {
         return await _context.Bookmarks.AnyAsync(x => x.Id == bookmarkId && x.Category.Space.userAccount.Id == userId);
      }

      public async Task<bool> IsBookmarkExistsAsync(Guid bookmarkId)
      {
         return await _context.Bookmarks.AnyAsync(x => x.Id == bookmarkId);
      }

      public async Task<BookmarkResponseDTO> UpdateBookmarkAsync(Guid bookmarkId, UpdateBookmarkDTO bookmark)
      {
         var bookmarkById = await _context.Bookmarks.FirstOrDefaultAsync(x => x.Id == bookmarkId);
         if(bookmarkById == null)
            throw new Exception("Bookmark not found.");
         bookmarkById.Title = bookmark.Title;
         bookmarkById.Description = bookmark.Description;
         bookmarkById.Url = bookmark.Url;
         bookmarkById.WebIcon = bookmark.WebIcon;
         await _context.SaveChangesAsync();
         return new BookmarkResponseDTO
         {
            Id = bookmarkById.Id,
            Title = bookmarkById.Title,
            Description = bookmarkById.Description,
            Url = bookmarkById.Url,
            WebIcon = bookmarkById.WebIcon,
            IconMineType = bookmarkById.IconMineType
         };
      }
   }
}