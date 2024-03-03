using Core.DTO.Category;

namespace Core.Domain.RepositoryContracts {
   public interface ICategoryRepository {
      Task<FullyCategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category);
      Task DeleteCategoryAsync(Guid categoryId);
      Task<FullyCategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId);
      Task<List<FullyCategoryResponseDTO>> GetCategoriesAsync(Guid spaceId);
      Task<FullyCategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category);
      Task<bool> IsCategoryExistsAsync(Guid categoryId);
      Task<bool> IsCategoryBelongToUserAsync(Guid userId, Guid space);
   }
}