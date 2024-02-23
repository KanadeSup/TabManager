using Core.Domain.Entities;
using Core.DTO;

namespace Core.ServiceContracts;
public interface ISpaceService
{
   Task<SpaceDTO> AddSpaceAsync(AddSpaceDTO space);
   Task<SpaceDTO> GetSpaceByIdAsync(Guid id);
   Task<List<SpaceDTO>> GetSpacesAsync();
   Task<SpaceDTO> UpdateSpaceAsync(Guid spaceId, UpdateSpaceDTO space);
   Task DeleteSpaceAsync(Guid id);
}