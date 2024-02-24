using Core.DTO.Category;

namespace Core.Domain.RepositoryContracts {
   public interface ICategoryRepository {
      Task<CategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category);
      Task DeleteCategoryAsync(Guid categoryId);
      Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId);
      Task<List<FullyCategoryResponseDTO>> GetCategoriesAsync(Guid spaceId);
      Task<CategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category);
      Task<bool> IsCategoryExistsAsync(Guid categoryId);
      Task<bool> IsCategoryBelongToUserAsync(Guid userId, Guid space);
   }
}