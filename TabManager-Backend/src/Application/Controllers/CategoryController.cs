using Core.DTO.Category;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
   [Authorize]
   public class CategoryController : ControllerBaseAPI
   {
      private readonly ICategoryService _categoryService;

      public CategoryController(ICategoryService categoryService)
      {
         _categoryService = categoryService;
      }

      [Route("/api/spaces/{spaceId}/categories")]
      [HttpGet]
      public async Task<IActionResult> GetCategoriesAsync(Guid spaceId)
      {
         var categories = await _categoryService.GetCategoriesAsync(spaceId);
         return Ok(categories);
      }

      [Route("/api/categories/{categoryId}")]
      [HttpGet]
      public async Task<IActionResult> GetCategoryAsync(Guid categoryId)
      {
         var category = await _categoryService.GetCategoryByIdAsync(categoryId);
         return Ok(category);
      }

      [Route("/api/spaces/{spaceId}/categories")]
      [HttpPost]
      public async Task<IActionResult> AddCategoryAsync(Guid spaceId, [FromBody] AddCategoryDTO category)
      {
         var categoryDTO = await _categoryService.AddCategoryAsync(spaceId, category);
         return Ok(categoryDTO);
      }
      
      [Route("/api/categories/{categoryId}")]
      [HttpDelete]
      public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
      {
         await _categoryService.DeleteCategoryAsync(categoryId);
         return Ok();
      }

      [Route("/api/categories/{categoryId}")]
      [HttpPut]
      public async Task<IActionResult> UpdateCategoryAsync(Guid categoryId, [FromBody] UpdateCategoryDTO category)
      {
         var categoryDTO = await _categoryService.UpdateCategoryAsync(categoryId, category);
         return Ok(categoryDTO);
      }

   }
}