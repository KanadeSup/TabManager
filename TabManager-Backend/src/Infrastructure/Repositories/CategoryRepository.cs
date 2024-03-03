using System.Data.Common;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO.Category;
using Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories {
   public class CategoryRepository : ICategoryRepository {
      private readonly AnnotationDbContext _context;
      public CategoryRepository(AnnotationDbContext context) {
         _context = context;
      }

      public async Task<FullyCategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category)
      {
         var spaceById = await _context.Spaces.FirstOrDefaultAsync(x => x.Id == spaceId);
         if(spaceById == null)
               throw new ArgumentException("spaceId is not valid");
         var newCategory = new Category {
            Id = Guid.NewGuid(),
            Name = category.Name,
            Space = spaceById,
         };
         await _context.Categories.AddAsync(newCategory);
         await _context.SaveChangesAsync();
         return new FullyCategoryResponseDTO {
            Id = newCategory.Id,
            Name = newCategory.Name,
            CreationTime = newCategory.CreationTime,
         };
      }

      public async Task DeleteCategoryAsync(Guid categoryId)
      {
         var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
         if(category == null)
               throw new ArgumentException("categoryId is not valid");
         _context.Categories.Remove(category);
         await _context.SaveChangesAsync();
      }

      public Task<List<FullyCategoryResponseDTO>> GetCategoriesAsync(Guid spaceId)
      {
         var hasSpace = _context.Spaces.AnyAsync(x => x.Id == spaceId);
         if(!hasSpace.Result)
               throw new ArgumentException("spaceId is not valid");

         return _context.Categories
            .Where(x => x.Space.Id == spaceId)
            .Select(x => new FullyCategoryResponseDTO {
               Id = x.Id,
               Name = x.Name,
               CreationTime = x.CreationTime,
               Bookmarks = x.Bookmarks.Select(y => new BookmarkResponseDTO {
                  Id = y.Id,
                  Title = y.Title,
                  Url = y.Url,
                  WebIcon = y.WebIcon,
                  Description = y.Description
               }).ToList(),
            }).ToListAsync();
      }

      public Task<FullyCategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId)
      {
         return _context.Categories
            .Where(x => x.Id == categoryId)
            .Select(x => new FullyCategoryResponseDTO {
               Id = x.Id,
               Name = x.Name,
               CreationTime = x.CreationTime,
               Bookmarks = x.Bookmarks.Select(y => new BookmarkResponseDTO {
                  Id = y.Id,
                  Description = y.Description,
                  Title = y.Title,
                  Url = y.Url,
                  WebIcon = y.WebIcon
               }).ToList()
            }).FirstOrDefaultAsync();
      }

      public Task<bool> IsCategoryBelongToUserAsync(Guid categoryId, Guid userId)
      {
         return _context.Categories.AnyAsync(x => x.Id == categoryId && x.Space.userAccount.Id == userId);
      }

      public Task<bool> IsCategoryExistsAsync(Guid categoryId)
      {
         return _context.Categories.AnyAsync(x => x.Id == categoryId);
      }

      public async Task<FullyCategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category)
      {
         var categoryById = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
         if(categoryById == null)
            throw new ArgumentException("categoryId is not valid");
         categoryById.Name = category.Name;
         _context.Categories.Update(categoryById);
         await _context.SaveChangesAsync();
         return new FullyCategoryResponseDTO {
            Id = categoryById.Id,
            Name = categoryById.Name,
            CreationTime = categoryById.CreationTime,
            Bookmarks = categoryById.Bookmarks.Select(x => new BookmarkResponseDTO {
               Id = x.Id,
               Description = x.Description,
               Title = x.Title,
               Url = x.Url,
               WebIcon = x.WebIcon
            }).ToList()
         };
      }
    }
}