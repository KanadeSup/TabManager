using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
   public class SpaceRepository : ISpaceRepository
   {
      private readonly AnnotationDbContext _context;
      public SpaceRepository(AnnotationDbContext context)
      {
         _context = context;
      }

      public async Task<SpaceDTO> AddSpaceAsync(AddSpaceDTO space, Guid userId)
      {
         var user = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == userId);
         if(user == null)
            throw new ArgumentException("userId is not valid");
         
         var newSpace = new Space {
            Name = space.Name,
            HexColor = space.HexColor,
            userAccount = user,
         };
         await _context.Spaces.AddAsync(newSpace);
         await _context.SaveChangesAsync();
         return new SpaceDTO {
            Id = newSpace.Id,
            Name = newSpace.Name,
            HexColor = newSpace.HexColor,
         };
      }

      public async Task DeleteSpaceAsync(Guid id)
      {
         var space = await _context.Spaces.FirstOrDefaultAsync(x => x.Id == id);
         if(space == null)
            throw new ArgumentException("id is not valid");
         _context.Spaces.Remove(space);
         await _context.SaveChangesAsync();
      }

      public async Task<SpaceDTO?> GetSpaceByIdAsync(Guid id)
      {
         var space = await _context.Spaces.FirstOrDefaultAsync(x => x.Id == id);
         if(space == null)
            return null;
         return new SpaceDTO {
            Id = space.Id,
            Name = space.Name,
            HexColor = space.HexColor,
         };
      }

      public async Task<List<SpaceDTO>> GetSpacesAsync(Guid userId)
      {
         var spaces = await _context.Spaces
            .Where(x => x.userAccount.Id == userId)
            .Select(s => new SpaceDTO{
               Id = s.Id,
               Name = s.Name,
               HexColor = s.HexColor,
            }).ToListAsync();
         return spaces;
      }

      public async Task<SpaceDTO> UpdateSpaceAsync(Guid spaceId, UpdateSpaceDTO space)
      {
         var spaceById = await _context.Spaces.FirstOrDefaultAsync(x => x.Id == spaceId);
         if(spaceById == null)
            throw new ArgumentException("id is not valid");

         spaceById.Name = space.Name;
         spaceById.HexColor = space.HexColor;

         _context.Spaces.Update(spaceById);
         await _context.SaveChangesAsync();
         
         return new SpaceDTO {
            Id = spaceId,
            Name = space.Name,
            HexColor = space.HexColor,
         };
      }

      public async Task<bool> IsSpaceExistsAsync(Guid id)
      {
         return await _context.Spaces.AnyAsync(x => x.Id == id);
      }

      public async Task<bool> IsSpaceBelongsToUserAsync(Guid spaceId, Guid userId)
      {
         return await _context.Spaces.AnyAsync(x => x.Id == spaceId && x.userAccount.Id == userId);
      }
   }
}