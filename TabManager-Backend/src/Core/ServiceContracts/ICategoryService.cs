using Core.DTO.Category;

namespace Core.ServiceContracts {
   public interface ICategoryService {
      Task<CategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category);
      Task DeleteCategoryAsync(Guid categoryId);
      Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId);
      Task<List<CategoryResponseDTO>> GetCategoriesAsync(Guid spaceId);
      Task<CategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category);
   }
}