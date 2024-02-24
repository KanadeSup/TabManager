using Common.Exceptions;
using Core.Domain.RepositoryContracts;
using Core.DTO.Category;
using Core.ServiceContracts;

namespace Core.Services {
   public class CategoryService : ICategoryService 
   {
      private readonly ICategoryRepository _categoryRepository;
      private readonly ICurrentUserService _currentUserService;
      private readonly ISpaceRepository _spaceRepository;
      public CategoryService(
         ICategoryRepository categoryRepository, 
         ICurrentUserService currentUserService,
         ISpaceRepository spaceRepository
      ) 
      {
         _categoryRepository = categoryRepository;
         _currentUserService = currentUserService;
         _spaceRepository = spaceRepository;
      }

      public async Task<CategoryResponseDTO> AddCategoryAsync(Guid spaceId, AddCategoryDTO category)
      {
         var userId = _currentUserService.UserId;
         if(userId == null) {
            throw new Exception("userId is not found in token");
         }
         if(await _spaceRepository.IsSpaceExistsAsync(spaceId) == false) {
            throw new SpaceNotFoundException("spaceId is not found");
         }
         if(await _spaceRepository.IsSpaceBelongsToUserAsync(spaceId, Guid.Parse(userId)) == false) {
            throw new SpaceNotFoundException("Space is not found");
         }
         var categoryDTO = await _categoryRepository.AddCategoryAsync(spaceId, category);
         return categoryDTO;
      }

      public async Task DeleteCategoryAsync(Guid categoryId)
      {
         var userId = _currentUserService.UserId;
         if(userId == null) {
            throw new Exception("userId is not found in token");
         }

         if(await _categoryRepository.IsCategoryExistsAsync(categoryId) == false) {
            throw new CategoryNotFoundException("category is not found");
         }

         if(await _categoryRepository.IsCategoryBelongToUserAsync(categoryId, Guid.Parse(userId)) == false) {
            throw new CategoryNotFoundException("Category is not found");
         }
         await _categoryRepository.DeleteCategoryAsync(categoryId);
      }

      public async Task<List<FullyCategoryResponseDTO>> GetCategoriesAsync(Guid spaceId)
      {
         var userId = _currentUserService.UserId;
         if(userId == null) {
            throw new Exception("userId is not found in token");
         }
         if(await _spaceRepository.IsSpaceExistsAsync(spaceId) == false) {
            throw new SpaceNotFoundException("space is not found");
         }
         if(await _spaceRepository.IsSpaceBelongsToUserAsync(spaceId, Guid.Parse(userId)) == false) {
            throw new SpaceNotFoundException("Space is not found");
         }
         var categories = await _categoryRepository.GetCategoriesAsync(spaceId);
         return categories;
      }

      public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId)
      {
         var userId = _currentUserService.UserId;
         if(userId == null) {
            throw new Exception("userId is not found in token");
         }

         if(await _categoryRepository.IsCategoryExistsAsync(categoryId) == false) {
            throw new CategoryNotFoundException("category is not found");
         }

         if(await _categoryRepository.IsCategoryBelongToUserAsync(categoryId, Guid.Parse(userId)) == false) {
            throw new CategoryNotFoundException("Category is not found");
         }

         var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
         return category;
      }

      public async Task<CategoryResponseDTO> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category)
      {
         var userId = _currentUserService.UserId;
         if(userId == null) {
            throw new Exception("userId is not found in token");
         }

         if(await _categoryRepository.IsCategoryExistsAsync(categoryId) == false) {
            throw new CategoryNotFoundException("category is not found");
         }

         if(await _categoryRepository.IsCategoryBelongToUserAsync(categoryId, Guid.Parse(userId)) == false) {
            throw new CategoryNotFoundException("Category is not found");
         }

         var categoryDTO = await _categoryRepository.UpdateCategoryAsync(categoryId, category);
         return categoryDTO;
      }
    }
}