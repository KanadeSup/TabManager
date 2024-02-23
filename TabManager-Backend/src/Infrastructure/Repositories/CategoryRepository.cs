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

      public async Task<CategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category)
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
         return new CategoryResponseDTO {
            Id = newCategory.Id,
            Name = newCategory.Name,
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

      public Task<List<CategoryResponseDTO>> GetCategoriesAsync(Guid spaceId)
      {
         var hasSpace = _context.Spaces.AnyAsync(x => x.Id == spaceId);
         if(!hasSpace.Result)
               throw new ArgumentException("spaceId is not valid");
         return _context.Categories
            .Where(x => x.Space.Id == spaceId)
            .Select(x => new CategoryResponseDTO {
               Id = x.Id,
               Name = x.Name,
            }).ToListAsync();
      }

      public Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId)
      {
         return _context.Categories
            .Where(x => x.Id == categoryId)
            .Select(x => new CategoryResponseDTO {
               Id = x.Id,
               Name = x.Name,
            }).FirstOrDefaultAsync();
      }

      public Task<bool> IsCategoryBelongsToSpaceAsync(Guid categoryId, Guid space)
      {
         return _context.Categories.AnyAsync(x => x.Id == categoryId && x.Space.Id == space);
      }

      public Task<bool> IsCategoryExistsAsync(Guid categoryId)
      {
         return _context.Categories.AnyAsync(x => x.Id == categoryId);
      }

        public async Task<CategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category)
      {
         var categoryById = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
         if(categoryById == null)
               throw new ArgumentException("categoryId is not valid");
         categoryById.Name = category.Name;
         _context.Categories.Update(categoryById);
         await _context.SaveChangesAsync();
         return new CategoryResponseDTO {
            Id = categoryById.Id,
            Name = categoryById.Name,
         };
      }
    }
}