using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ReservationsDbContext _context;

        public SpaceRepository(ReservationsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Space space)
        {
            await _context.Spaces.AddAsync(space);
        }

        public async Task DeleteAsync(Guid id)
        {
            var space = await _context.Spaces.FindAsync(id);
            if (space != null)
            {
                _context.Spaces.Remove(space);
            }
        }

        public async Task<IEnumerable<Space>> GetAllAsync()
        {
            return await _context.Spaces.ToListAsync();
        }

        public async Task<Space?> GetByIdAsync(Guid id)
        {
            return await _context.Spaces.AsNoTracking().FirstOrDefaultAsync(s=> s.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Space space)
        {
            _context.Entry(space).CurrentValues.SetValues(space);
            _context.Spaces.Update(space);
        }
    }
}