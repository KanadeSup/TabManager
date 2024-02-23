using Core.Domain.RepositoryContracts;
using Core.DTO;
using Core.ServiceContracts;
using Common.Exceptions;

namespace Core.Services;

public class SpaceService : ISpaceService
{
   private readonly ISpaceRepository _spaceRepository;
   private readonly ICurrentUserService _currentUserService;

   public SpaceService(ISpaceRepository spaceRepository, ICurrentUserService currentUserService)
   {
      _spaceRepository = spaceRepository;
      _currentUserService = currentUserService;

   }
   public async Task<SpaceDTO> AddSpaceAsync(AddSpaceDTO space)
   {
      var userId = _currentUserService.UserId;
      if(userId == null) {
         throw new Exception("userId is not found in token");
      }
         
      var spaceDTO = await _spaceRepository.AddSpaceAsync(space, Guid.Parse(userId));
      return spaceDTO;
   }

   public async Task DeleteSpaceAsync(Guid spaceId)
   {
      var userId = _currentUserService.UserId;
      if(userId == null) {
         throw new Exception("userId is not found in token");
      }

      // Check if the space belongs to the user
      if(await _spaceRepository.IsSpaceBelongsToUserAsync(spaceId, Guid.Parse(userId)) == false) {
         throw new SpaceNotFoundException("Space is not found");
      }
      var hasSpace = await _spaceRepository.IsSpaceExistsAsync(spaceId);
      
      if(!hasSpace) {
         throw new SpaceNotFoundException("Space is not found");
      }
      await _spaceRepository.DeleteSpaceAsync(spaceId);
   }

   public async Task<SpaceDTO> GetSpaceByIdAsync(Guid spaceId)
   {
      var userId = _currentUserService.UserId;
      if(userId == null) {
         throw new Exception("userId is not found in token");
      }
      if(await _spaceRepository.IsSpaceBelongsToUserAsync(spaceId, Guid.Parse(userId)) == false) {
         throw new SpaceNotFoundException("Space is not found");
      }

      var space = await _spaceRepository.GetSpaceByIdAsync(spaceId);
      if(space == null) {
         throw new SpaceNotFoundException("Space is not found");
      }
      return space;
   }

   public async Task<List<SpaceDTO>> GetSpacesAsync()
   {
      var userId = _currentUserService.UserId;
      if(userId == null) {
         throw new Exception("userId is not found in token");
      }
      return await _spaceRepository.GetSpacesAsync(Guid.Parse(userId));
   }

   public async Task<SpaceDTO> UpdateSpaceAsync(Guid spaceId, UpdateSpaceDTO space)
   {
      var userId = _currentUserService.UserId;
      if(userId == null) {
         throw new Exception("userId is not found in token");
      }

      // Check if the space belongs to the user
      bool isSpaceBelongsToUser = await _spaceRepository.IsSpaceBelongsToUserAsync(spaceId, Guid.Parse(userId));
      if(!isSpaceBelongsToUser) {
         throw new SpaceNotFoundException("Space is not found");
      }
      return await _spaceRepository.UpdateSpaceAsync(spaceId, space);
   }
}