using System.Security.Claims;
using Core.DTO;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
   [Authorize]
   public class SpaceController : ControllerBaseAPI
   {
      private readonly ISpaceService _spaceService;

      public SpaceController(ISpaceService spaceService)
      {
         _spaceService = spaceService;
      }

      [HttpGet]
      public async Task<IActionResult> GetSpaces()
      {
         var spaces = await _spaceService.GetSpacesAsync();
         return Ok(spaces);
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetSpaceById(Guid id)
      {
         var space = await _spaceService.GetSpaceByIdAsync(id);
         return Ok(space);
      }

      [HttpPost]
      public async Task<IActionResult> AddSpace([FromBody] AddSpaceDTO space)
      {
         var spaceDTO = await _spaceService.AddSpaceAsync(space);
         return Ok(spaceDTO);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateSpace([FromRoute] Guid id, [FromBody] UpdateSpaceDTO space)
      {
         var spaceDTO = await _spaceService.UpdateSpaceAsync(id, space);
         return Ok(spaceDTO);
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteSpace([FromRoute]Guid id)
      {
         await _spaceService.DeleteSpaceAsync(id);
         return NoContent();
      }

   }
}