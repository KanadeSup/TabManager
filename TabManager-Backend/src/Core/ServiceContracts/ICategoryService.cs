using Core.DTO.Category;

namespace Core.ServiceContracts {
   public interface ICategoryService {
      Task<FullyCategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category);
      Task DeleteCategoryAsync(Guid categoryId);
      Task<FullyCategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId);
      Task<List<FullyCategoryResponseDTO>> GetCategoriesAsync(Guid spaceId);
      Task<FullyCategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category);
   }
}