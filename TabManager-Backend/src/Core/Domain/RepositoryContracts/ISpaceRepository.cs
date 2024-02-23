using Core.DTO;

namespace Core.Domain.RepositoryContracts {
   public interface ISpaceRepository {
      Task<List<SpaceDTO>> GetSpacesAsync(Guid userId);
      Task<SpaceDTO?> GetSpaceByIdAsync(Guid id);
      Task<SpaceDTO> AddSpaceAsync(AddSpaceDTO space, Guid userId);
      Task<SpaceDTO> UpdateSpaceAsync(Guid spaceId, UpdateSpaceDTO space);
      Task DeleteSpaceAsync(Guid id);
      Task<bool> IsSpaceExistsAsync(Guid id);
      Task<bool> IsSpaceBelongsToUserAsync(Guid spaceId, Guid userId);
   }
}