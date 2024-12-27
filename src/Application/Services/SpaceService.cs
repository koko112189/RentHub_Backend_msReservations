using Application.Interfaces;
using Domain.Exceptions.SpaceExceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository _spaceRepository;

        public SpaceService(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task CreateSpaceAsync(Space space)
        {
            var existingSpace = await _spaceRepository.GetByIdAsync(space.Id);

            if (existingSpace != null)
            {
                throw new SpaceAlreadyExistsException(space.Name);
            }

            await _spaceRepository.AddAsync(space);
            await _spaceRepository.SaveChangesAsync();
        }

        public async Task DeleteSpaceAsync(Guid id)
        {
            var existingSpace = await _spaceRepository.GetByIdAsync(id);

            if (existingSpace == null)
            {
                throw new SpaceNotFoundException(id);
            }

            await _spaceRepository.DeleteAsync(id);
            await _spaceRepository.SaveChangesAsync();
        }

        public async Task<Space?> GetSpaceByIdAsync(Guid id)
        {
            var space = await _spaceRepository.GetByIdAsync(id);

            if (space == null)
            {
                throw new SpaceNotFoundException(id);
            }

            return space;
        }

        public async Task<IEnumerable<Space>> GetSpacesAsync()
        {
            return await _spaceRepository.GetAllAsync();
        }

        public async Task UpdateSpaceAsync(Space space)
        {
            var existingSpace = await _spaceRepository.GetByIdAsync(space.Id);

            if (existingSpace == null)
            {
                throw new SpaceNotFoundException(space.Id);
            }

            await _spaceRepository.UpdateAsync(space);
            await _spaceRepository.SaveChangesAsync();
        }
    }
}