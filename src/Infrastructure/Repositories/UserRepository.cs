using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ReservationsDbContext _context;

        public UserRepository(ReservationsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User User)
        {
            await _context.Users.AddAsync(User);
        }

        public async Task DeleteAsync(Guid id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                _context.Users.Remove(User);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(s=> s.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User User)
        {
            _context.Entry(User).CurrentValues.SetValues(User);
            _context.Users.Update(User);
        }
    }
}